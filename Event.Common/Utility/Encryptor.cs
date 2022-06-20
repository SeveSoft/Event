using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace Event.Common.Utility
{
    public static class Encryptor
    {
        private static byte[] _iv; // The initial vector 
        private static byte[] _key; // The key 
        private static string _pass = "rf829f3nff086j9p"; // The pass for generating keys 
        private static byte[] _salt = { 159, 169, 8, 225, 128, 161, 251, 200, 192, 244, 128, 231, 36, 155, 165, 55 };

        static Encryptor()
        {
            GenerateKeys();
        }
        /// <summary> 
        /// Generates the initial vector and key 
        /// </summary> 
        private static void GenerateKeys()
        {
            var keyGenerator = new Rfc2898DeriveBytes(_pass, _salt);
            _key = keyGenerator.GetBytes(16);
            _iv = keyGenerator.GetBytes(16);
        }
        /// <summary> 
        /// Encrypts a string 
        /// </summary> 
        /// <param name="plainText"></param> 
        /// <returns></returns> 
        public static string Encrypt(string plainText)
        {
            return Encrypt(plainText, _iv, _key);
        }
        /// <summary> 
        /// Encrypts a int 
        /// </summary> 
        /// <param name="i"></param> 
        /// <returns></returns> 
        public static string Encrypt(int i)
        {
            return Encrypt(i.ToString(), _iv, _key);
        }

        public static string Encrypt(long i)
        {
            return Encrypt(i.ToString(), _iv, _key);
        }

        /// <summary> 
        /// Decrypts a string 
        /// </summary> 
        /// <param name="cipherText"></param> 
        /// <returns></returns> 
        public static string DecryptString(string cipherText)
        {
            return Decrypt(cipherText, _iv, _key);
        }
        /// <summary> 
        /// Encrypts a string using the supplied IV and key. 
        /// </summary> 
        /// <param name="plainText"></param> 
        /// <param name="iv"></param> 
        /// <param name="key"></param> 
        /// <returns></returns> 
        private static string Encrypt(string plainText, byte[] iv, byte[] key)
        {
            var aesProvider = new AesCryptoServiceProvider();
            ICryptoTransform cryptoEncryptor = aesProvider.CreateEncryptor(key, iv);
            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, cryptoEncryptor, CryptoStreamMode.Write))
                {
                    //Convert the string to a byte array. 
                    byte[] plainTextBytes = Encoding.Default.GetBytes(plainText);
                    //Write all data to the crypto stream and flush it. 
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] cipherTextBytes = memoryStream.ToArray();
                    //Convert the byte array to a string. 
                    string cipherText = Convert.ToBase64String(cipherTextBytes);
                    return cipherText;
                }
            }
        }
        /// <summary> 
        /// Decrypts a string using the supplied IV and key. 
        /// </summary> 
        /// <param name="cipherText"></param> 
        /// <param name="iv"></param> 
        /// <param name="key"></param> 
        /// <returns></returns> 
        private static string Decrypt(string cipherText, byte[] iv, byte[] key)
        {
            var aesProvider = new AesCryptoServiceProvider();
            //Convert the string to a byte array. 
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            ICryptoTransform cryptoDecryptor = aesProvider.CreateDecryptor(key, iv);
            using (var memoryStream = new MemoryStream(cipherTextBytes))
            {
                using (var cryptoStream = new CryptoStream(memoryStream, cryptoDecryptor, CryptoStreamMode.Read))
                {
                    var plainTextBytes = new byte[cipherTextBytes.Length];
                    //Read all data from the crypto stream. 
                    int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                    //Convert the byte array to a string. 
                    string plainText = Encoding.Default.GetString(plainTextBytes, 0, decryptedByteCount);
                    return plainText;
                }
            }
        }
    }
}
