using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Repository.Base
{
    public interface IUnitOfWork : IDisposable
    {
        int Commit();

        Task<int> CommitAsync();

        List<Q> RawSqlQuery<Q>(string query, Func<DbDataReader, Q> map);

        DbContext getContext();
    }
}
