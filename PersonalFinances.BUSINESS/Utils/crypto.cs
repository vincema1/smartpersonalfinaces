using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;


namespace PersonalFinances.BUSINESS.Utils
{
    public static class crypto
    {
        public static string ComputeHash(string input)
        {
            HashAlgorithm sha = SHA256.Create();
            byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(input));
            return Convert.ToBase64String(hashData);
        }

        public static bool VerifyHash(string input, string hashValue)
        {
            return ComputeHash(input) == hashValue;
        }


    }
}