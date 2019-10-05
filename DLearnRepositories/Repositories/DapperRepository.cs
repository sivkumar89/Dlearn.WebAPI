using DapperExtensions;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;

namespace DLearnRepositories.Repositories
{
    public class DapperRepository<T> : IDapperRepository<T> where T : class
    {
        private readonly IDbConnection _dbConnection;
        private int ConnectionTimeout { get; set; }
        public DapperRepository(IDbConnection dBConnection)
        {
            _dbConnection = dBConnection;
        }
        public bool Insert(T parameter)
        {
            _dbConnection.Open();
            _dbConnection.Insert(parameter);
            _dbConnection.Close();
            return true;
        }

        public int InsertWithReturnId(T parameter)
        {
            _dbConnection.Open();
            var recordId = _dbConnection.Insert(parameter);
            _dbConnection.Close();
            return recordId;
        }

        public Guid InsertWithReturnGuidId(T parameter)
        {
            _dbConnection.Open();
            var recordId = _dbConnection.Insert(parameter);
            _dbConnection.Close();
            return recordId;
        }

        public bool Update(T parameter)
        {
            _dbConnection.Open();
            _dbConnection.Update(parameter);
            _dbConnection.Close();
            return true;
        }

        public T Get(int id)
        {
            _dbConnection.Open();
            var result = _dbConnection.Get<T>(id);
            _dbConnection.Close();
            return result;
        }

        public T Get(long id)
        {
            _dbConnection.Open();
            var result = _dbConnection.Get<T>(id);
            _dbConnection.Close();
            return result;
        }

        public T Get(Guid id)
        {
            _dbConnection.Open();
            var result = _dbConnection.Get<T>(id);
            _dbConnection.Close();
            return result;
        }

        public IEnumerable<T> GetAll()
        {
            _dbConnection.Open();
            var result = _dbConnection.GetList<T>();
            _dbConnection.Close();
            return result.ToList();
        }

        public IEnumerable<T> Find(PredicateGroup predicate)
        {
            _dbConnection.Open();
            var result = _dbConnection.GetList<T>(predicate);
            _dbConnection.Close();
            return result;
        }

        public bool Delete(PredicateGroup predicate)
        {
            _dbConnection.Open();
            _dbConnection.Delete<T>(predicate);
            _dbConnection.Close();
            return true;
        }

        private static void CombineParameters(ref dynamic param, dynamic outParam = null)
        {
            if (outParam != null)
            {
                if (param != null)
                {
                    param = new DynamicParameters(param);
                    ((DynamicParameters)param).AddDynamicParams(outParam);
                }
                else
                {
                    param = outParam;
                }
            }
        }

        private int GetTimeout(int? commandTimeout = null)
        {
            if (commandTimeout.HasValue)
                return commandTimeout.Value;

            return ConnectionTimeout;
        }
    }
}
