using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace Repository.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The DbContext
        /// </summary>
        private DbContext _dbContext;

        public DbContext getContext()
        {
            return this._dbContext;
        }

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="context">The object context</param>
        public UnitOfWork(DbContext context)
        {
            _dbContext = context;
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int Commit()
        {
            try
            {
                // Save changes with the default options
                return _dbContext.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                var error = CheckDbError(ex);
                throw new DbUpdateException(error, new Exception(error));
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                // Save changes with the default options
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                var error = CheckDbError(ex);
                throw new DbUpdateException(error, new Exception(error));
            }
        }


        public List<Q> RawSqlQuery<Q>(string query, Func<DbDataReader, Q> map)
        {

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = CommandType.Text;

                _dbContext.Database.OpenConnection();

                using (var result = command.ExecuteReader())
                {
                    var entities = new List<Q>();

                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }

                    return entities;
                }
            }

        }

        bool disposed = false;
        /// <summary>
        /// Disposes the current object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing && _dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
            }
            disposed = true;
        }

        private string CheckDbError(DbUpdateException e)
        {
            const int SqlServerViolationOfUniqueConstraint = 2627;
            const int SqlServerViolationOfForeignKeyConstraint = 547;
            var exception = e?.InnerException as Microsoft.Data.SqlClient.SqlException;

            return (exception?.Number) switch
            {
                SqlServerViolationOfForeignKeyConstraint => "SqlServerViolationOfForeignKeyConstraint",
                SqlServerViolationOfUniqueConstraint => "SqlServerViolationOfUniqueConstraint",
                _ => String.Format("SqlServerUnknown", e?.Message + ":" + e?.InnerException?.Message),
            };
        }
    }
}
