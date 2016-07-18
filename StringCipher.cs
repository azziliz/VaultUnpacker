using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Vault
{
    public static class StringCipher
    {
        private const string _initVector = "tu89geji340t89u2";
        private const int _keySize = 256;
        private const int _passPhraseLength = 8;

        public static string Encrypt(string plainText, string passPhrase)
        {
            byte[] bytes1 = Encoding.UTF8.GetBytes("tu89geji340t89u2");
            byte[] bytes2 = Encoding.UTF8.GetBytes(plainText);
            byte[] bytes3 = new Rfc2898DeriveBytes(passPhrase, bytes1).GetBytes(32);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = rijndaelManaged.CreateEncryptor(bytes3, bytes1);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(bytes2, 0, bytes2.Length);
            cryptoStream.FlushFinalBlock();
            byte[] inArray = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(inArray);
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            byte[] bytes1 = Encoding.ASCII.GetBytes("tu89geji340t89u2");
            byte[] buffer = Convert.FromBase64String(cipherText);
            byte[] bytes2 = new Rfc2898DeriveBytes(passPhrase, Encoding.ASCII.GetBytes("tu89geji340t89u2")).GetBytes(32);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            rijndaelManaged.Mode = CipherMode.CBC;
            ICryptoTransform decryptor = rijndaelManaged.CreateDecryptor(bytes2, bytes1);
            MemoryStream memoryStream = new MemoryStream(buffer);
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] numArray = new byte[buffer.Length];
            int count = cryptoStream.Read(numArray, 0, numArray.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(numArray, 0, count);
        }

        public static string GeneratePassPhrase(string plainText)
        {
            string plainText1 = plainText;
            while (plainText1.Length < 8)
                plainText1 = plainText1 + plainText;
            return StringCipher.Base64Encode(plainText1).Substring(0, 8);
        }

        public static string Base64Encode(string plainText)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
        }

        public static string Base64Decode(string encodedText)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedText));
        }
    }
}
