using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Base
{
    public interface IRepository<T> : IDisposable where T : class
    {
        DbContext Context { get; }

        string Provider { get; }

        void Delete(T item);

        void DeleteRange(IEnumerable<T> items);

        IQueryable<T> GetAll();

        T GetById(int id);

        T GetById(object[] values);

        Task<T> GetByIdAsync(object[] values);

        Task<T> GetByIdAsync(int id);

        void Insert(T item);

        void InsertRange(IEnumerable<T> items);

        void Update(T item);

        void SetProp(T item, string propName, Object newValue);

        IQueryable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null,
                          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

    }
}
