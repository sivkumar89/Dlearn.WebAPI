using System;

namespace DLearnServices.Entities
{
    public class UserValidationEntity
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
    }
}
