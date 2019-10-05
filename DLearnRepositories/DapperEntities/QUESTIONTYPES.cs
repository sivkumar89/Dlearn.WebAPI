using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLearnRepositories.DapperEntities
{
    [Table("COURSE.QUESTIONTYPES")]
    public class QUESTIONTYPES
    {
        public int QUESTIONTYPEID { get; set; }
        public string QUESTIONTYPE { get; set; }
        public bool ISACTIVE { get; set; }
        public Guid CREATEDBY { get; set; }
        public Guid? LASTMODIFIEDBY { get; set; }
        public DateTime CREATEDON { get; set; }
        public DateTime? LASTMODIFIEDON { get; set; }
    }
}
