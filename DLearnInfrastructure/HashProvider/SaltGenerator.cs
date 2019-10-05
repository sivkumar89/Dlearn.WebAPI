using System;
using System.Security.Cryptography;

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
