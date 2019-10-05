using Dapper.Contrib.Extensions;
using System;

namespace DLearnRepositories.DapperEntities
{
    [Table("COURSE.COURSEOBJECTIVES")]
    public class COURSEOBJECTIVES
    {
        public long COURSEOBJECTIVEID { get; set; }
        public long COURSEID { get; set; }
        public int QUESTIONTYPEID { get; set; }
        public string ANSWER { get; set; }
        public int? DISPLAYORDER { get; set; }
        public int? DURATION { get; set; }
        public bool ISACTIVE { get; set; }
        public Guid CREATEDBY { get; set; }
        public Guid? LASTMODIFIEDBY { get; set; }
        public DateTime CREATEDON { get; set; }
        public DateTime? LASTMODIFIEDON { get; set; }
    }
}
