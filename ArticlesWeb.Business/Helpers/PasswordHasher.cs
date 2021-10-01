using System;
using System.Linq;
using System.Text;

namespace ArticlesWeb.Business.Helpers
{
    public class PasswordHasher
    {
        public (string hPassword, string hSalt) CreatePasswordHash(string password)
        {
            byte[] passwordHash, passwordSalt;
            
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            var pass = Convert.ToBase64String(passwordHash);
            var salt = Convert.ToBase64String(passwordSalt);
            return (pass, salt);
        }

        public bool VerifyPasswordHash(string password, string passwordHash, string passwordSalt)
        {
            var saltBytes = Convert.FromBase64String(passwordSalt);
            var passwordBytes = Convert.FromBase64String(passwordHash);

            using (var hmac = new System.Security.Cryptography.HMACSHA512(saltBytes))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                if (!computedHash.SequenceEqual(passwordBytes)) return false;
            }
            return true;
        }
    }
}