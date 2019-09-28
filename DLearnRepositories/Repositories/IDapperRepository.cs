using DapperExtensions;
using System.Collections.Generic;

namespace DLearnRepositories.Repositories
{
    public interface IDapperRepository<T> where T : class
    {
        bool Insert(T parameter);
        int InsertWithReturnId(T parameter);
        bool Update(T parameter);
        IList<T> GetAll();
        T Find(PredicateGroup predicate);
        bool Delete(PredicateGroup predicate);
    }
}
