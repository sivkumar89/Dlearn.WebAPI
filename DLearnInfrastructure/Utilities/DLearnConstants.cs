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

        #endregion


        #region Enums
        public enum DLearnErrorMessage
        {
            INVALIDCREDENTIALS = 1,
            USERNOTFOUND = 2
        }
        #endregion
    }
}
