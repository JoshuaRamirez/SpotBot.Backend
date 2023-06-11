using System.Security.Cryptography;

namespace SpotBot.Server.Core
{
    public class Encryption
    {
        private readonly string _key;

        public Encryption(string key) {
            _key = key;
        }

        public static string GenerateSymmetricKey()
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.GenerateKey();
                return Convert.ToBase64String(aes.Key);
            }
        }

        public string Encrypt(string value)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(_key);
                aes.GenerateIV();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    memoryStream.Write(aes.IV, 0, aes.IV.Length);

                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(value);
                        }
                    }
                    var array = memoryStream.ToArray();
                    return Convert.ToBase64String(array);
                }
            }
        }

        public string Decrypt(string value)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = Convert.FromBase64String(_key);
                var valueBytes = Convert.FromBase64String(value);
                var iv = new byte[16];
                var cipher = new byte[valueBytes.Length - iv.Length];
                Buffer.BlockCopy(valueBytes, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(valueBytes, iv.Length, cipher, 0, cipher.Length);
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                var decryptor = aes.CreateDecryptor();

                using (MemoryStream memoryStream = new MemoryStream(cipher))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            var decrypted = streamReader.ReadToEnd();
                            return decrypted;
                        }
                    }
                }
            }
        }

    }
}
