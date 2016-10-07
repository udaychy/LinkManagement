using System;
using System.Security.Cryptography;
using System.Text;

namespace LinkManagement.BL
{
    public class Helper
    {
        public string GetHasedValue(string plainText, string hashAlgorithm = "SHA1")
        {
            byte[] bytes = Encoding.Unicode.GetBytes(plainText);
            byte[] inArray = HashAlgorithm.Create(hashAlgorithm).ComputeHash(bytes);

            return (Convert.ToBase64String(inArray));
        }
    }
}