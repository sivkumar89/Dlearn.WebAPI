using DapperExtensions;
using System;
using System.Collections.Generic;

namespace DLearnRepositories.Repositories
{
    public interface IDapperRepository<T> where T : class
    {
        bool Insert(T parameter);
        int InsertWithReturnId(T parameter);
        long InsertWithReturnLongId(T parameter);
        Guid InsertWithReturnGuidId(T parameter);
        bool Update(T parameter);
        T Get(int id);
        T Get(long id);
        T Get(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(PredicateGroup predicate);
        bool Delete(PredicateGroup predicate);
    }
}
