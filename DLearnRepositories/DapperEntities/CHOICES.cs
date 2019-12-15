using Dapper.Contrib.Extensions;
using System;

namespace DLearnRepositories.DapperEntities
{
    [Table("COURSE.CHOICES")]
    public class CHOICES
    {
        public long CHOICEID { get; set; }
        public string CHOICEDESC { get; set; }
        public long COURSEOBJECTIVEID { get; set; }
        public int? DISPLAYORDER { get; set; }
        public bool ISACTIVE { get; set; }
        public DateTime CREATEDON { get; set; }
        public DateTime? LASTMODIFIEDON { get; set; }
    }
}
