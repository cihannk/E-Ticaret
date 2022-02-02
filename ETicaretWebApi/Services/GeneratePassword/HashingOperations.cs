using ETicaretWebApi.DbOperations;
using ETicaretWebApi.Entitites;
using PasswordGenerator;
using System.Security.Cryptography;
using System.Text;

namespace ETicaretWebApi.Services.GeneratePassword
{
    public class HashingOperations
    {
        public static void GenerateHash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public static bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i<computedHash.Length; i++)
                {
                    if (passwordHash[i] != computedHash[i])
                        return false;
                }
                return true;
            }
        }
    }
}
