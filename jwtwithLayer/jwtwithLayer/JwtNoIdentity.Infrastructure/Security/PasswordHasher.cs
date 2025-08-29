using System.Security.Cryptography;

namespace jwtwithLayer.JwtNoIdentity.Infrastructure.Security
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;     // 128-bit
        private const int KeySize = 32;      // 256-bit
        private const int Iterations = 100_000; // PBKDF2 iterations

        public static (string hash, string salt) Hash(string password)
        {
            // Step 1: Salt generate karo
            using var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[SaltSize];
            rng.GetBytes(saltBytes);

            // Step 2: Password + Salt ka hash banao
            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            // Step 3: Hash aur Salt ko return karo (string format me)
            return (Convert.ToBase64String(key), Convert.ToBase64String(saltBytes));
        }
        public static bool varify(string password, string storedHash, string storedSalt)
        {
            // Convert stored salt to bytes
            var saltBytes = Convert.FromBase64String(storedSalt);

            // Recompute hash using input password + stored salt
            using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            // Compare computed hash with stored hash
            return CryptographicOperations.FixedTimeEquals(key, Convert.FromBase64String(storedHash));
        }


    }
}
