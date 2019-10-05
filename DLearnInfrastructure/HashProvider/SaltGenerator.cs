using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DLearnInfrastructure.HashProvider
{
    public class SaltGenerator
    {
        public static string CreateRandomSalt()
        {
            byte[] saltBytes = new byte[64];

            var rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(saltBytes);
            var saltText = BitConverter.ToString(saltBytes);

            return saltText;
        }
    }
}
