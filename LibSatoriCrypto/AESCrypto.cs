using LibSatoriCrypto.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace LibSatoriCrypto
{
    public class AESCrypto
    {
        public static void EncryptFile(BinaryReader reader, BinaryWriter writer, char[] password, char[] filename, int offset, ICryptoProgress progress)
        {
            progress.Max = 4;
            progress.Now = 1;
            AesManaged aes = new AesManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.IV = Encoding.UTF8.GetBytes(GetIV(filename));
            aes.Key = Encoding.UTF8.GetBytes(GetKey(password));
            aes.Padding = PaddingMode.PKCS7;

            progress.Now = 2;
            byte[] byteText = reader.ReadBytes((int)reader.BaseStream.Length - offset);
            progress.Now = 3;
            byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);
            progress.Now = 4;
            writer.Write(encryptText);
        }

        public static byte[] GetEncryptedBinary(BinaryReader reader, char[] password, char[] filename, int offset, ICryptoProgress progress)
        {
            progress.Max = 4;
            progress.Now = 1;
            AesManaged aes = new AesManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.IV = Encoding.UTF8.GetBytes(GetIV(filename));
            aes.Key = Encoding.UTF8.GetBytes(GetKey(password));
            aes.Padding = PaddingMode.PKCS7;

            progress.Now = 2;
            reader.BaseStream.Seek(offset, SeekOrigin.Begin);
            byte[] byteText = reader.ReadBytes((int)reader.BaseStream.Length - offset);
            progress.Now = 3;
            byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);
            progress.Now = 4;
            return encryptText;
        }

        public static byte[] DecryptFile(BinaryReader reader, BinaryWriter writer, char[] password, char[] filename, int offset, ICryptoProgress progress)
        {
            progress.Max = 4;
            progress.Now = 1;
            AesManaged aes = new AesManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.IV = Encoding.UTF8.GetBytes(GetIV(filename));
            aes.Key = Encoding.UTF8.GetBytes(GetKey(password));
            aes.Padding = PaddingMode.PKCS7;

            progress.Now = 2;
            byte[] byteText = reader.ReadBytes((int)reader.BaseStream.Length - offset);
            progress.Now = 3;
            byte[] encryptText = aes.CreateEncryptor().TransformFinalBlock(byteText, 0, byteText.Length);
            progress.Now = 4;
            return encryptText;
        }

        public static byte[] GetDecryptedBinary(BinaryReader reader, char[] password, char[] filename, int offset, ICryptoProgress progress)
        {
            return GetDecryptedBinary(reader.ReadBytes((int)reader.BaseStream.Length - offset), password, filename, offset, progress);
        }

        public static byte[] GetDecryptedBinary(byte[] targetBinary, char[] password, char[] filename, int offset, ICryptoProgress progress)
        {
            progress.Max = 4;
            progress.Now = 1;
            AesManaged aes = new AesManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.IV = Encoding.UTF8.GetBytes(GetIV(filename));
            aes.Key = Encoding.UTF8.GetBytes(GetKey(password));
            aes.Padding = PaddingMode.PKCS7;

            progress.Now = 2;
            progress.Now = 3;
            byte[] decryptText = aes.CreateDecryptor().TransformFinalBlock(targetBinary, 0, targetBinary.Length);
            progress.Now = 4;
            return decryptText;
        }

        public static char[] GetIV(char[] baseIV)
        {
            List<char> iv = new();
            for (int i = 0; i < 16; i++)
            {
                if (baseIV.Length <= i) iv.Add((char)0);
                else iv.Add(baseIV[i]);
            }

            return iv.ToArray();
        }

        public static char[] GetKey(char[] baseKey)
        {
            List<char> key = new();
            for (int i = 0; i < 32; i++)
            {
                if (baseKey.Length <= i) key.Add((char)0);
                else key.Add(baseKey[i]);
            }

            return key.ToArray();
        }
    }
}
