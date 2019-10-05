using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLearnRepositories.DapperEntities
{
    [Table("COURSE.COURSES")]
    public class COURSES
    {
        public long COURSEID { get; set; }
        public int COURSECATEGORYID { get; set; }
        public string COURSENAME { get; set; }
        public string DESCRIPTION { get; set; }
        public long? COURSEIMAGEID { get; set; }
        public string STATUS { get; set; }
        public int? COURSEDURATION { get; set; }
        public bool ISACTIVE { get; set; }
        public Guid CREATEDBY { get; set; }
        public Guid? LASTMODIFIEDBY { get; set; }
        public DateTime CREATEDON { get; set; }
        public DateTime? LASTMODIFIEDON { get; set; }
    }
}
