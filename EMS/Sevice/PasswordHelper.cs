using EMS.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace EMS.Sevice
{
    public class PasswordHelper
    {
        public string HashPassword(string password)
        {
            var hash = new PasswordHasher<UserData>();
            return hash.HashPassword(null,password);
        }
        public bool VerifyPassword(string password, string hash)
        {
            var hashp = new PasswordHasher<UserData>();
            var result = hashp.VerifyHashedPassword(null, hash, password);
            return result ==PasswordVerificationResult.Success? true:false;
        }
    }
}
