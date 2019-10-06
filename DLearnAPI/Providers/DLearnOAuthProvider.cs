using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using DLearnInfrastructure.HashProvider;
using DLearnInfrastructure.Utilities;
using DLearnServices.Entities;
using DLearnServices.Interfaces;
using static DLearnInfrastructure.Utilities.DLearnConstants;

namespace DLearnAPI.Providers
{
    #region DLearn OAuth Authorization Server Provider
    public class DLearnOAuthProvider: OAuthAuthorizationServerProvider
    {
        #region Private Declarations
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public DLearnOAuthProvider(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region OAuth Authorization Server Provider Methods
        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ClaimsIdentity identity = new ClaimsIdentity(DLearnConstants.DefaultClaimName);
            Guid userId = context.OwinContext.Get<Guid>(DLearnConstants.DefaultClaimName + DLearnConstants.ClaimsUserId);
            string email = context.OwinContext.Get<string>(DLearnConstants.DefaultClaimName + DLearnConstants.ClaimsEmailId);
            string fullName = context.OwinContext.Get<string>(DLearnConstants.DefaultClaimName + DLearnConstants.ClaimsUsername);
            identity.AddClaim(new Claim(ClaimTypes.Name, fullName, ClaimValueTypes.String));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId.ToString(), ClaimValueTypes.String));
            identity.AddClaim(new Claim(ClaimTypes.Email, email, ClaimValueTypes.String));
            //identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, "role"));
            context.Validated(identity);
            return Task.FromResult(0);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                string userName = context.Parameters["username"];
                string password = context.Parameters["password"];
                string loginType = context.Parameters["logintype"];

                if (!string.IsNullOrWhiteSpace(userName) && Utility.IsValidEmail(userName) && !string.IsNullOrWhiteSpace(password))
                {
                    if (string.IsNullOrWhiteSpace(loginType))
                    {
                        UserValidationEntity userDetail = _userService.GetUserDetailsByEmail(userName);
                        if (userDetail != null)
                        {
                            string hashedKey = HashGenerator.CreateHashedKey(password, userDetail.Salt);
                            if (!string.IsNullOrWhiteSpace(hashedKey) && hashedKey == userDetail.PasswordHash)
                            {
                                context.OwinContext.Set(DLearnConstants.DefaultClaimName + DLearnConstants.ClaimsUsername, userDetail.FullName);
                                context.OwinContext.Set(DLearnConstants.DefaultClaimName + DLearnConstants.ClaimsUserId, userDetail.UserId);
                                context.OwinContext.Set(DLearnConstants.DefaultClaimName + DLearnConstants.ClaimsEmailId, userDetail.Email);
                                context.Validated();
                            }
                            else
                            {
                                HandleLoginError(context, DLearnErrorMessage.INVALIDCREDENTIALS);
                            }
                        }
                        else
                        {
                            HandleLoginError(context, DLearnErrorMessage.USERNOTFOUND);
                        }
                    }
                }
                else
                {
                    HandleLoginError(context, DLearnErrorMessage.INVALIDCREDENTIALS);
                }
            }
            catch (Exception ex)
            {
                context.SetError("Server error", ex.Message + (ex.InnerException != null ? " Cause:" + ex.InnerException.Message : string.Empty));
                context.Rejected();
            }
            return Task.FromResult(0);
        }
        #endregion

        #region OAuth Authorization Server Provider Private Methods
        private void HandleLoginError(OAuthValidateClientAuthenticationContext context, DLearnErrorMessage errorCode)
        {
            switch (errorCode)
            {
                case DLearnErrorMessage.INVALIDCREDENTIALS:
                    context.SetError("Invalid credentials");
                    break;
                case DLearnErrorMessage.USERNOTFOUND:
                    context.SetError("User not found");
                    break;
                default:
                    context.SetError("Internal Server Error");
                    break;
            }
            context.Rejected();
        }
        #endregion
    }
    #endregion
}