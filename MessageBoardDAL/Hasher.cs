using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBoardDAL
{
    public static class Hasher
    {
        public static string Hash(string plaintext)
        {
            byte[] bytes = new UTF8Encoding().GetBytes(plaintext);
            using (var sha512 = SHA512CryptoServiceProvider.Create())
            {
                byte[] hashedBytes = sha512.ComputeHash(bytes);
                string hashedString = BitConverter.ToString(hashedBytes);
                return hashedString.Replace("-", "").ToLower();
            }
        }
    }
}
