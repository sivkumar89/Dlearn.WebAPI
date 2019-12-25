using Dapper.Contrib.Extensions;
using System;

namespace DLearnRepositories.DapperEntities
{
    [Table("dbo.USERADDRESS")]
    public class USERADDRESS
    {
        public long ADDRESSID { get; set; }
        public Guid USERID { get; set; }
        public string ADDRESSLINE1 { get; set; }
        public string ADDRESSLINE2 { get; set; }
        public string LANDMARK { get; set; }
        public string CITY { get; set; }
        public int STATEID { get; set; }
        public string COUNTRYCODE { get; set; }
        public int PINCODE { get; set; }
        public DateTime CREATEDON { get; set; }
        public DateTime? LASTMODIFIED { get; set; }

    }
}
