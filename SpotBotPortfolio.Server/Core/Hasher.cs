using System.Security.Cryptography;

namespace SpotBot.Server.Core
{
    internal class Hasher
    {
        public static string Hash(string password)
        {
            // Generate a random salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Derive a 256-bit PBKDF2 key (32 bytes) from the password and salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32);

            // Combine the salt and hash into a single byte array
            byte[] hashBytes = new byte[48];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);

            // Convert the byte array to a base64 string
            string passwordHash = Convert.ToBase64String(hashBytes);

            return passwordHash;
        }

        public static bool VerifyPassword(string password, string storedHash)
        {
            // Convert the base64 string to a byte array
            byte[] storedHashBytes = Convert.FromBase64String(storedHash);

            // Get the salt from the hash bytes
            byte[] storedSalt = new byte[16];
            Array.Copy(storedHashBytes, 0, storedSalt, 0, 16);

            // Compute the hash on the password the user entered
            var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, 10000, HashAlgorithmName.SHA256);
            var providedHashBytes = pbkdf2.GetBytes(32);

            // Compare the computed hash with the stored hash
            for (int i = 0; i < 32; i++)
            {
                if (storedHashBytes[i + 16] != providedHashBytes[i])
                {
                    return false;
                }
            }

            return true;
        }

    }
}
