using Dapper.Contrib.Extensions;
using System;

namespace DLearnRepositories.DapperEntities
{
    [Table("COURSE.ANSWER")]
    public class ANSWER
    {
        public long ANSWERID { get; set; }
        public long COURSEOBJECTIVEID { get; set; }
        public string ANSWERDESC { get; set; }
        public DateTime CREATEDON { get; set; }
        public DateTime? LASTMODIFIEDON { get; set; }
    }
}
