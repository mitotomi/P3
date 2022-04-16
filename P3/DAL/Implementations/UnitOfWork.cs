using Microsoft.EntityFrameworkCore;
using P3.DAL.Contracts;
using P3.Models.EFModels;

namespace P3.DAL.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly P3Context dbContext;
        private readonly IRepository repositoryProvider;

        public UnitOfWork(P3Context dbContext)
        {
            this.dbContext = dbContext;

            if (repositoryProvider == null)
            {
                repositoryProvider = new RepositoryProvider();
            }

            repositoryProvider.DbContext = dbContext;
        }

        /// <summary>
        /// This does NOT support parallel query execution.
        /// Basically a single Context can NOT do parallel queries
        /// and we should manually override it by creating new context on every request.
        /// This can only be done by MANUALLY creating a new DbContext, or setting up so that every call gets a new instance from Scope,
        /// but in such way you can't do ForEach
        /// https://stackoverflow.com/questions/50788272/how-to-instantiate-a-dbcontext-in-ef-core
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IGenericRepository<T> GetGenericRepository<T>() where T : EntityBase
        {
            return repositoryProvider.GetGenericRepository<T>();
        }

        public async Task<int> SaveAsync(bool allowHardDelete = false)
        {
            return await dbContext.SaveEntityChangesAsync(allowHardDelete);
        }

        public int Save(bool allowHardDelete = false)
        {
            return dbContext.SaveEntityChanges(allowHardDelete);
        }

        public void Rollback()
        {
            dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        /// <summary>
        /// Don't use it unless you absolutely must
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> GetQuery<T>() where T : class
        {
            return dbContext.Set<T>().AsQueryable().AsNoTracking();
        }

        /// <summary>
        /// Don't use this unless you absolutely must
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DbSet<T> GetDbSet<T>() where T : class
        {
            return dbContext.Set<T>();
        }
    }
}
