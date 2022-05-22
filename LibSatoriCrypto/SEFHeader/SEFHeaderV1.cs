/* --- Satori Encrypted File Header V1 ---
 * 0x00: byte[3] シグネチャ (SEF\0)
 * 0x04: int     バージョン (常に1)
 * 0x08: int     多重暗号化方式
 * 0x0C: int     暗号化データへのオフセット (最初からの位置)
 */

namespace LibSatoriCrypto.SEFHeader
{
    public class SEFHeaderV1
    {
        /// <summary>
        /// SEFヘッダーV1を読み込み、構造体に変換します。
        /// </summary>
        /// <param name="binReader">バイナリリーダー</param>
        /// <returns>変換されたSEFヘッダーV1構造体</returns>
        /// <exception cref="InvalidDataException">シグネチャが正しくないときにスローされます。</exception>
        public static SEFHeaderV1Struct ReadSefHeaderV1(BinaryReader binReader)
        {
            if (binReader.BaseStream.Length < 16) throw new Exception("Not enough length (min 16 byte)");

            long backup_pos = binReader.BaseStream.Position;
            binReader.BaseStream.Position = 0;


            byte[] signature = binReader.ReadBytes(4);
            if (signature[0] != 0x53 || signature[1] != 0x45 || signature[2] != 0x46 || signature[3] != 0x0)
            {
                throw new InvalidDataException("Invalid SEFV1 Signature");
            }

            binReader.BaseStream.Seek(4, SeekOrigin.Current);

            var sefheader = new SEFHeaderV1Struct

            {
                SuperencryptionMode = (SuperencryptionMode)binReader.ReadInt32(),
                DataOffset = binReader.ReadInt32()
            };

            binReader.BaseStream.Position = backup_pos;
            return sefheader;
        }

        /// <summary>
        /// 構造体からSEFヘッダーV1を作成します。
        /// </summary>
        /// <param name="sefHeaderV1">SEFヘッダーの情報</param>
        /// <returns>生成されたバイト配列のSEFヘッダーV1</returns>
        public static byte[] CreateSefHeaderV1(SEFHeaderV1Struct sefHeaderV1)
        {
            List<byte> sefheader = new()
            {
                // シグネチャ (SEF\0)
                0x53,
                0x45,
                0x46,
                0x00,
                // バージョン (int32: 1)
                0x01,
                0x00,
                0x00,
                0x00
            };

            switch (sefHeaderV1.SuperencryptionMode)
            {
                case SuperencryptionMode.None:
                    sefheader.Add(0x00);
                    sefheader.Add(0x00);
                    sefheader.Add(0x00);
                    sefheader.Add(0x00);
                    break;

                case SuperencryptionMode.PxF2Key:
                    sefheader.Add(0x01);
                    sefheader.Add(0x00);
                    sefheader.Add(0x00);
                    sefheader.Add(0x00);
                    break;

                case SuperencryptionMode.AES:
                    sefheader.Add(0x02);
                    sefheader.Add(0x00);
                    sefheader.Add(0x00);
                    sefheader.Add(0x00);
                    break;

                case SuperencryptionMode.PxAES:
                    sefheader.Add(0x03);
                    sefheader.Add(0x00);
                    sefheader.Add(0x00);
                    sefheader.Add(0x00);
                    break;
            }

            byte[] offset_byte = BitConverter.GetBytes(sefHeaderV1.DataOffset);
            for (int i = 0; i < offset_byte.Length; i++) sefheader.Add(offset_byte[i]);
            return sefheader.ToArray();
        }
    }

    public struct SEFHeaderV1Struct
    {
        /// <summary>
        /// 多重暗号化方式
        /// </summary>
        public SuperencryptionMode SuperencryptionMode { get; init; }

        /// <summary>
        /// 暗号化されたデータへのオフセット
        /// </summary>
        public int DataOffset { get; init; }
    }
}
