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
    #region Unit of Work
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Private Declarations
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        private readonly string _connectionString;

        private IDapperRepository<USERS> _userRepository;
        private IDapperRepository<COURSECATEGORY> _courseCategoryRepository;
        private IDapperRepository<COURSEOBJECTIVES> _courseObjectiveRepository;
        private IDapperRepository<COURSES> _courseRepository;
        private IDapperRepository<CHOICES> _choiceRepository;
        private IDapperRepository<ANSWER> _answerRepository;
        #endregion

        #region Constructor
        public UnitOfWork()
        {
            _connectionString = Utility.GetConnectionString(RepositoryConstants.DLearnConnectionString);
            _connection = new SqlConnection(_connectionString);
        }
        #endregion

        #region Properties

        #region User Repository
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
        #endregion

        #region Course Category Repository
        public IDapperRepository<COURSECATEGORY> CourseCategoryRepository
        {
            get
            {
                if (_courseCategoryRepository == null)
                {
                    _courseCategoryRepository = new DapperRepository<COURSECATEGORY>(_connection);
                }
                return _courseCategoryRepository;
            }
        }
        #endregion

        #region Course Objective Repository
        public IDapperRepository<COURSEOBJECTIVES> CourseObjectiveRepository
        {
            get
            {
                if (_courseObjectiveRepository == null)
                {
                    _courseObjectiveRepository = new DapperRepository<COURSEOBJECTIVES>(_connection);
                }
                return _courseObjectiveRepository;
            }
        }
        #endregion

        #region Course Repository
        public IDapperRepository<COURSES> CourseRepository
        {
            get
            {
                if (_courseRepository == null)
                {
                    _courseRepository = new DapperRepository<COURSES>(_connection);
                }
                return _courseRepository;
            }
        }
        #endregion

        #region Choices Repository
        public IDapperRepository<CHOICES> ChoiceRepository
        {
            get
            {
                if (_choiceRepository == null)
                {
                    _choiceRepository = new DapperRepository<CHOICES>(_connection);
                }
                return _choiceRepository;
            }
        }
        #endregion

        #region Answer Repository
        public IDapperRepository<ANSWER> AnswerRepository
        {
            get
            {
                if (_answerRepository == null)
                {
                    _answerRepository = new DapperRepository<ANSWER>(_connection);
                }
                return _answerRepository;
            }
        }
        #endregion

        #endregion

        #region Unit of Work Methods

        #region Database Transaction Methods
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
        #endregion

        #region Stored Procedure Methods
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
        #endregion

        #region Dispose
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
        #endregion

        #endregion
    }
    #endregion
}