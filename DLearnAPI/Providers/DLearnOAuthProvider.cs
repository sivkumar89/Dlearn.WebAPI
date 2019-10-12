using System;
using System.Net;
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
                string userName = context.Parameters[DLearnConstants.UserName];
                string password = context.Parameters[DLearnConstants.Password];
                string loginType = context.Parameters[DLearnConstants.LoginType];

                if (!string.IsNullOrWhiteSpace(userName) && Utility.IsValidEmail(userName) && !string.IsNullOrWhiteSpace(password))
                {
                    if (!string.IsNullOrWhiteSpace(loginType) && loginType == SignInType.Register.ToString())
                    {
                        UserCreateRequestEntity userCreateRequest = new UserCreateRequestEntity
                        {
                            Email = userName,
                            Salt = SaltGenerator.CreateRandomSalt(),
                            FirstName = context.Parameters[DLearnConstants.FirstName],
                            LastName = context.Parameters[DLearnConstants.LastName],
                            DOB = !string.IsNullOrWhiteSpace(context.Parameters[DLearnConstants.DOB]) ? DateTime.ParseExact(context.Parameters[DLearnConstants.DOB], "yyyy-MM-dd", null) : DateTime.MinValue,
                            Gender = context.Parameters[DLearnConstants.Gender],
                            Phone = context.Parameters[DLearnConstants.Phone],
                            SubscriptionType = context.Parameters[DLearnConstants.SubscriptionType]
                        };
                        userCreateRequest.FullName = userCreateRequest.FirstName + (!string.IsNullOrWhiteSpace(userCreateRequest.LastName) ? " " + userCreateRequest.LastName : string.Empty);
                        userCreateRequest.PasswordHash = HashGenerator.CreateHashedKey(password, userCreateRequest.Salt);
                        Guid userID = _userService.CreateUser(userCreateRequest);
                        if (userID != Guid.Empty)
                        {
                            GrantUserCredentials(ref context, userCreateRequest.FullName, userCreateRequest.Email, userID);
                        }
                        else
                        {
                            HandleLoginError(ref context, DLearnErrorMessage.INTERNALSERVERERROR);
                        }
                    }
                    else
                    {
                        UserValidationEntity userDetail = _userService.GetUserDetailsByEmail(userName);
                        if (userDetail != null)
                        {
                            string hashedKey = HashGenerator.CreateHashedKey(password, userDetail.Salt);
                            if (!string.IsNullOrWhiteSpace(hashedKey) && hashedKey == userDetail.PasswordHash)
                            {
                                _userService.UpdateUserTimestamp(userDetail.UserId);
                                GrantUserCredentials(ref context, userDetail.FullName, userDetail.Email, userDetail.UserId);
                            }
                            else
                            {
                                HandleLoginError(ref context, DLearnErrorMessage.INVALIDCREDENTIALS);
                            }
                        }
                        else
                        {
                            HandleLoginError(ref context, DLearnErrorMessage.USERNOTFOUND);
                        }
                    }
                }
                else
                {
                    HandleLoginError(ref context, DLearnErrorMessage.INVALIDCREDENTIALS);
                }
            }
            catch (Exception ex)
            {
                context.SetError("Server error", ex.Message + (ex.InnerException != null ? " Cause:" + ex.InnerException.Message : string.Empty));
                context.Response.Headers.Add(DLearnConstants.OwinChallengeFlag, new[] { ((int)HttpStatusCode.InternalServerError).ToString() });

            }
            return Task.FromResult(0);
        }
        #endregion

        #region OAuth Authorization Server Provider Private Methods
        private void HandleLoginError(ref OAuthValidateClientAuthenticationContext context, DLearnErrorMessage errorCode)
        {
            switch (errorCode)
            {
                case DLearnErrorMessage.INVALIDCREDENTIALS:
                    context.SetError("Invalid credentials");
                    context.Response.Headers.Add(DLearnConstants.OwinChallengeFlag, new[] { ((int)HttpStatusCode.Unauthorized).ToString() });
                    break;
                case DLearnErrorMessage.USERNOTFOUND:
                    context.SetError("User not found");
                    context.Response.Headers.Add(DLearnConstants.OwinChallengeFlag, new[] { ((int)HttpStatusCode.NotFound).ToString() });
                    break;
                default:
                    context.SetError("Internal Server Error");
                    context.Response.Headers.Add(DLearnConstants.OwinChallengeFlag, new[] { ((int)HttpStatusCode.InternalServerError).ToString() });
                    break;
            }
        }
        #endregion

        #region Set User Claims
        private void GrantUserCredentials(ref OAuthValidateClientAuthenticationContext context, string fullName, string emailID, Guid userID)
        {
            context.OwinContext.Set(DLearnConstants.DefaultClaimName + DLearnConstants.ClaimsUsername, fullName);
            context.OwinContext.Set(DLearnConstants.DefaultClaimName + DLearnConstants.ClaimsUserId, userID);
            context.OwinContext.Set(DLearnConstants.DefaultClaimName + DLearnConstants.ClaimsEmailId, emailID);
            context.Validated();
        }
        #endregion
    }
    #endregion
}