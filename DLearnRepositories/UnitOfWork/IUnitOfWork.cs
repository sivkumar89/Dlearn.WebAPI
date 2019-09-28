using DLearnRepositories.DapperEntities;
using DLearnRepositories.Repositories;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DLearnRepositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IDapperRepository<USERS> UserRepository { get; }
        void BeginTransaction();
        void EndTransaction();
        void QuerySP(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null);
        IEnumerable<T> QuerySP<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null) where T : class;
    }
}
