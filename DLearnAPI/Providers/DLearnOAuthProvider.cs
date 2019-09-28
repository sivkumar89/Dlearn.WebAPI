using DLearnInfrastructure.Utilities;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DLearnAPI.Providers
{
    public class DLearnOAuthProvider: OAuthAuthorizationServerProvider
    {
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(DLearnConstants.DefaultClaimName);
            var username = context.OwinContext.Get<string>(DLearnConstants.DefaultClaimName + DLearnConstants.ClaimsUsername);
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, username));
            identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "role"));
            context.Validated(identity);
            return Task.FromResult(0);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                var username = context.Parameters["username"];
                var password = context.Parameters["password"];

                if (username == password)
                {
                    context.OwinContext.Set(DLearnConstants.DefaultClaimName + DLearnConstants.ClaimsUsername, username);
                    context.Validated();
                }
                else
                {
                    context.SetError("Invalid credentials");
                    context.Rejected();
                }
            }
            catch
            {
                context.SetError("Server error");
                context.Rejected();
            }
            return Task.FromResult(0);
        }
    }
}