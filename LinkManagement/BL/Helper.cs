using System;
using System.Net;
using System.Net.Sockets;
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

        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}