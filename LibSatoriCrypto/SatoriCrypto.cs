using LibSatoriCrypto.Interface;
using LibSatoriCrypto.SEFHeader;

namespace LibSatoriCrypto
{
    public class SatoriCrypto
    {
        /// <summary>
        /// ファイルを暗号化します。
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="newfilename"></param>
        /// <param name="password"></param>
        /// <param name="superencryptionMode"></param>
        public static void EncryptFile(string filename, string newfilename, string password, SuperencryptionMode superencryptionMode, ICryptoProgress progress)
        {
            using (FileStream fromFileStream = new FileStream(filename, FileMode.Open))
            using (FileStream toFileStream = new FileStream(newfilename, FileMode.OpenOrCreate))
            using (BinaryReader fromBinaryReader = new BinaryReader(fromFileStream))
            using (BinaryWriter toBinaryWriter = new BinaryWriter(toFileStream))
            {
                char[] key = GenerateKey(password, Path.GetFileName(filename), superencryptionMode).ToCharArray();
                byte[] sefheader = SEFHeaderV1.CreateSefHeaderV1(new SEFHeaderV1Struct
                {
                    DataOffset = 16,
                    SuperencryptionMode = superencryptionMode,
                });

                toBinaryWriter.Seek(0, SeekOrigin.Begin);
                toBinaryWriter.Write(sefheader);
                toBinaryWriter.Seek(16, SeekOrigin.Begin);

                switch (superencryptionMode)
                {
                    case SuperencryptionMode.PxF2Key:
                    case SuperencryptionMode.None:
                        XORBinaryData(fromBinaryReader, toBinaryWriter, key, 0, progress);
                        break;

                    case SuperencryptionMode.AES:
                        AESCrypto.EncryptFile(fromBinaryReader, toBinaryWriter, key, Path.GetFileName(filename).ToArray(), 0, progress);
                        break;

                    case SuperencryptionMode.PxAES:
                        byte[] aesEncrypted = AESCrypto.GetEncryptedBinary(fromBinaryReader, key, Path.GetFileName(filename).ToArray(), 0, progress);
                        toBinaryWriter.Write(GetXORBinaryData(aesEncrypted, key, progress));
                        break;
                }
                

                toBinaryWriter.Flush();
            }
        }

        public static async Task EncryptFileAsync(string filename, string newfilename, string password, SuperencryptionMode superencryptionMode, ICryptoProgress progress)
        {
            await Task.Run(() => EncryptFile(filename, newfilename, password, superencryptionMode, progress));
        }

        /// <summary>
        /// ファイルを復号化します。多重暗号化方式は、復号化するファイルのSEFHeaderから自動で取得されます。
        /// </summary>
        /// <param name="filename">復号化するファイルパス</param>
        /// <param name="newfilename">復号化したファイルを書き込むパス</param>
        /// <param name="password">パスワード</param>
        public static void DecryptFile(string filename, string newfilename, string password, ICryptoProgress progress)
        {
            using (FileStream fromFileStream = new FileStream(filename, FileMode.Open))
            using (FileStream toFileStream = new FileStream(newfilename, FileMode.OpenOrCreate))
            using (BinaryReader fromBinaryReader = new BinaryReader(fromFileStream))
            using (BinaryWriter toBinaryWriter = new BinaryWriter(toFileStream))
            {
                SEFHeaderV1Struct sefheader = SEFHeaderV1.ReadSefHeaderV1(fromBinaryReader);

                string filenameWithoutSef = null;
                if (filename.EndsWith(".sef")) filenameWithoutSef = Path.GetFileName(filename)[0..^4];
                else filenameWithoutSef = Path.GetFileName(filename);

                char[] key = GenerateKey(password, filenameWithoutSef, sefheader.SuperencryptionMode).ToCharArray();

                switch (sefheader.SuperencryptionMode)
                {
                    case SuperencryptionMode.PxF2Key:
                    case SuperencryptionMode.None:
                        XORBinaryData(fromBinaryReader, toBinaryWriter, key, sefheader.DataOffset, progress);
                        break;

                    case SuperencryptionMode.AES:
                        AESCrypto.DecryptFile(fromBinaryReader, toBinaryWriter, key, filenameWithoutSef.ToArray(), sefheader.DataOffset, progress);
                        break;

                    case SuperencryptionMode.PxAES:
                        fromBinaryReader.BaseStream.Seek(sefheader.DataOffset, SeekOrigin.Begin);
                        byte[] xoredBinary = GetXORBinaryData(fromBinaryReader.ReadBytes((int)fromBinaryReader.BaseStream.Length - sefheader.DataOffset), key, progress);
                        byte[] aesDecrypted = AESCrypto.GetDecryptedBinary(xoredBinary, key, filenameWithoutSef.ToArray(), sefheader.DataOffset, progress);
                        toBinaryWriter.Write(aesDecrypted);
                        break;
                }
                
                toBinaryWriter.Flush();
            }
        }

        public static async Task DecryptFileAsync(string filename, string newfilename, string password, ICryptoProgress progress)
        {
            await Task.Run(() => DecryptFile(filename, newfilename, password, progress));
        }

        /// <summary>
        /// バイナリデータをXORします。
        /// </summary>
        /// <param name="reader">XORするターゲットのBinaryReader</param>
        /// <param name="writer">XORしたデータを書き込むBinaryWriter</param>
        /// <param name="xorKey">XORするキー</param>
        /// <param name="offset">XORを始めるオフセット</param>
        private static void XORBinaryData(BinaryReader reader, BinaryWriter writer, char[] xorKey, int offset, ICryptoProgress progress)
        {
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);

            writer.Write(GetXORBinaryData(reader.ReadBytes((int)reader.BaseStream.Length - offset), xorKey, progress));
        }

        private static void XORBinaryData(byte[] targetBytes, BinaryWriter writer, char[] xorKey, ICryptoProgress progress)
        {
            writer.Write(GetXORBinaryData(targetBytes, xorKey, progress));
        }

        private static byte[] GetXORBinaryData(byte[] targetBytes, char[] xorKey, ICryptoProgress progress)
        {
            int key_nowpos = 0;

            progress.Max = targetBytes.Length;
            progress.Now = 0;

            for (int i = 0; i < targetBytes.Length - 1; i++)
            {
                if (i % 10240000 == 0) progress.Now = i;
                targetBytes[i] ^= (byte)xorKey[key_nowpos];

                key_nowpos++;
                if (key_nowpos > xorKey.Length - 1) key_nowpos = 0;
            }
            progress.Now = progress.Max;

            return targetBytes;
        }

        /// <summary>
        /// 多重暗号化方式毎にキーを生成します。
        /// </summary>
        /// <param name="password">パスワード</param>
        /// <param name="othervalue">その多重暗号化方式が取るキー</param>
        /// <param name="mode">多重暗号化方式</param>
        /// <returns></returns>
        private static string GenerateKey(string password, string othervalue, SuperencryptionMode mode)
        {
            if (string.IsNullOrEmpty(password)) return "\0"; //パスワードがない場合は無条件で \0 を返す。

            switch (mode)
            {
                case SuperencryptionMode.PxF2Key:
                    char[] keys = othervalue.ToCharArray();
                    char[] password_char = password.ToCharArray();

                    int password_char_nowpos = 0;
                    for (int i = 0; i < keys.Length; i++)
                    {
                        keys[i] ^= password_char[password_char_nowpos];
                        password_char_nowpos++;
                        if (password_char_nowpos > password_char.Length - 1) password_char_nowpos = 0;
                    }

                    return new string(keys);

                default:
                    return password;
            }

            return password;
        }

        public static bool IsValidSEF(string filename)
        {
            using (BinaryReader binreader = new BinaryReader(File.OpenRead(filename)))
            {
                if (binreader.ReadUInt32() == 4605267) return true;
                else return false;
            }
        }
    }
}