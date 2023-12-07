using System.Security.Cryptography;

namespace Bushido.TestTask.Cloud.Authentication.Services
{
    public static class PasswordHasher
    {
        // Generate a random salt
        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }


        public static string HashPassword(string password)
        {
            byte[] salt = GenerateSalt();
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32); // 32 bytes for SHA256
                byte[] hashBytes = new byte[32 + 32]; // 32 bytes for salt
                Array.Copy(salt, 0, hashBytes, 0, 32);
                Array.Copy(hash, 0, hashBytes, 32, 32);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[32];
            Array.Copy(hashBytes, 0, salt, 0, 32);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                for (int i = 0; i < 32; i++)
                {
                    if (hashBytes[i + 32] != hash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

    }
}
