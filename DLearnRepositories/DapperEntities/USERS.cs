using Dapper.Contrib.Extensions;
using System;

namespace DLearnRepositories.DapperEntities
{
    [Table("dbo.USERS")]
    public class USERS
    {
        public Guid USERID { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string FULLNAME { get; set; }
        public string GENDER { get; set; }
        public DateTime DATEOFBIRTH { get; set; }
        public string PASSWORDHASH { get; set; }
        public string SALT { get; set; }
        public string EMAIL { get; set; }
        public string PHONENUMBER { get; set; }
        public string SUBSCRIPTIONTYPE { get; set; }
        public bool ISACTIVE { get; set; }
        public DateTime CREATEDON { get; set; }
        public DateTime? LASTMODIFIED { get; set; }
        public DateTime? LASTLOGON { get; set; }

    }
}
