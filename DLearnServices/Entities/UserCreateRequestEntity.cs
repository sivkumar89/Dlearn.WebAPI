namespace DLearnServices.Entities
{
    public class UserCreateRequestEntity : UserEntity
    {
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
