using System;

namespace DLearnServices.Entities
{
    public class UserEntity
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string SubscriptionType { get; set; }
    }
}
