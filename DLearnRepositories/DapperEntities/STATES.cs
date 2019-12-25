using Dapper.Contrib.Extensions;

namespace DLearnRepositories.DapperEntities
{
    [Table("dbo.STATES")]
    public class STATES
    {
        public int STATEID { get; set; }
        public string STATENAME { get; set; }
        public bool ISACTIVE { get; set; }
    }
}
