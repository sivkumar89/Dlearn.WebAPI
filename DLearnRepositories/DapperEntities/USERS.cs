using System;

namespace DLearnRepositories.DapperEntities
{
    public class USERS
    {
        public Guid USER_ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string FULL_NAME { get; set; }
        public string GENDER { get; set; }
        public DateTime DATE_OF_BIRTH { get; set; }
        public string PASSWORD_HASH { get; set; }
        public string EMAIL { get; set; }
        public string PHONE_NUMBER { get; set; }
        public long? ADDRESS_ID { get; set; }
        public long? IMAGE_ID { get; set; }
        public string SUBSCRIPTION_TYPE { get; set; }
        public bool IS_ACTIVE { get; set; }
        public DateTime CREATED_ON { get; set; }
        public DateTime? LAST_MODIFIED { get; set; }
        public DateTime? LAST_LOGON { get; set; }

    }
}
