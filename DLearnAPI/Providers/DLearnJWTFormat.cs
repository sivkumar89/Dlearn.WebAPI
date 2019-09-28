using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using DLearnInfrastructure.Utilities;
using System.Security.Claims;

namespace DLearnAPI.Providers
{
    public class DLearnJWTFormat : ISecureDataFormat<AuthenticationTicket>
    {
        public string SignatureAlgorithm => SecurityAlgorithms.HmacSha256Signature;
        public string DigestAlgorithm => SecurityAlgorithms.Sha256Digest; 

        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException("data");

            byte[] symmetricKey = Convert.FromBase64String(DLearnConstants.SecretKey);

            SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SignatureAlgorithm, DigestAlgorithm);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "User"),
                new Claim(ClaimTypes.NameIdentifier, "UserID"),
            });

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = DateTime.Now.AddMinutes(Convert.ToInt32(DLearnInfrastructure.Utilities.Utility.GetAppSettings(DLearnConstants.TokenExpireInMinutes))),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var plainToken = tokenHandler.CreateToken(securityTokenDescriptor);
            return tokenHandler.WriteToken(plainToken);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}