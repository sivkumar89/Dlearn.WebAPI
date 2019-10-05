using Dapper.Contrib.Extensions;
using System;

namespace DLearnRepositories.DapperEntities
{
    [Table("COURSE.COURSECATEGORY")]
    public class COURSECATEGORY
    {
        public int COURSECATEGORYID { get; set; }
        public string CATEGORYNAME { get; set; }
        public string DESCRIPTION { get; set; }
        public bool ISACTIVE { get; set; }
        public Guid CREATEDBY { get; set; }
        public Guid? LASTMODIFIEDBY { get; set; }
        public DateTime CREATEDON { get; set; }
        public DateTime? LASTMODIFIEDON { get; set; }
    }
}
