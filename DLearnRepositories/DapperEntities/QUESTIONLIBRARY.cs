using Dapper.Contrib.Extensions;
using System;

namespace DLearnRepositories.DapperEntities
{
    [Table("COURSE.QUESTIONLIBRARY")]
    public class QUESTIONLIBRARY
    {
        public long QUESTIONLIBRARYID { get; set; }
        public long COURSEOBJECTIVEID { get; set; }
        public string OBJECTIVEOPTION { get; set; }
        public int DISPLAYORDER { get; set; }
        public bool ISACTIVE { get; set; }
        public Guid CREATEDBY { get; set; }
        public Guid? LASTMODIFIEDBY { get; set; }
        public DateTime CREATEDON { get; set; }
        public DateTime? LASTMODIFIEDON { get; set; }
    }
}
