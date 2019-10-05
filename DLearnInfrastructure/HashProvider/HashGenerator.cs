using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DLearnInfrastructure.HashProvider
{
    public class HashGenerator
    {

        public static string CreateHashedKey(string pswd, string salt)
        {
            byte[] pswdBytes = Encoding.UTF8.GetBytes(pswd);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] pswdWithSaltBytes = new byte[pswdBytes.Length + saltBytes.Length];

            for (int i = 0; i < pswdBytes.Length; i++)
            {
                pswdWithSaltBytes[i] = pswdBytes[i];
            }

            for (int i = 0; i < saltBytes.Length; i++)
            {
                pswdWithSaltBytes[pswdBytes.Length + i] = saltBytes[i];
            }

            SHA512 cryptoProvider = SHA512.Create();
            byte[] hashedBytes = cryptoProvider.ComputeHash(pswdWithSaltBytes);

            var hashedText = BitConverter.ToString(hashedBytes);

            return hashedText;
        }
    }
}
