namespace DLearnInfrastructure.Utilities
{
    public class DLearnConstants
    {
        #region Constants

        public const string SecretKey = "f89ccc6fce5abebaa106b782845e101810a76879aa964bc9be8ea3a5de9a01e2";
        public const string TokenExpireInMinutes = "TokenExpireInMinutes";
        public const string DefaultClaimName = "dlearn";
        public const string ClaimsUsername = ":fullname";
        public const string ClaimsUserId = ":userid";
        public const string ClaimsEmailId = ":email";
        public const string UserName = "username";
        public const string Password = "password";
        public const string LoginType = "logintype";
        public const string FirstName = "firstname";
        public const string LastName = "lastname";
        public const string DOB = "dob";
        public const string Gender = "gender";
        public const string Phone = "phone";
        public const string SubscriptionType = "subscription";
        public const string OwinChallengeFlag = "X-Challenge";

        #endregion


        #region Enums
        public enum DLearnErrorMessage
        {
            INVALIDCREDENTIALS = 1,
            USERNOTFOUND = 2,
            INTERNALSERVERERROR = 3
        }

        public enum SignInType
        {
            Login = 1,
            Register = 2
        }
        #endregion
    }
}
