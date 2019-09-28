using Dapper;
using DLearnRepositories.DapperEntities;
using DLearnRepositories.Repositories;
using DLearnRepositories.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DLearnRepositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly string _connectionString;

        private IDapperRepository<USERS> _userRepository;

        public UnitOfWork()
        {
            _connectionString = Utility.GetConnectionString(RepositoryConstants.DLearnConnectionString);
            _connection = new SqlConnection(_connectionString);
        }

        public IDapperRepository<USERS> UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new DapperRepository<USERS>(_connection);
                }
                return _userRepository;
            }
        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }

        public void EndTransaction()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void QuerySP(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null)
        {
            _connection.Open();
            _connection.Query(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);
        }
        public IEnumerable<T> QuerySP<T>(string storedProcedure, dynamic param = null, dynamic outParam = null, SqlTransaction transaction = null, bool buffered = true, int? commandTimeout = null) where T : class
        {
            _connection.Open();
            IEnumerable<T> output = _connection.Query<T>(storedProcedure, param: (object)param, transaction: transaction, buffered: buffered, commandTimeout: commandTimeout, commandType: CommandType.StoredProcedure);
            return output;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}