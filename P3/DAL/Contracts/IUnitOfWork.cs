using Microsoft.EntityFrameworkCore;
using P3.Models.EFModels;

namespace P3.DAL.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GetGenericRepository<T>() where T : EntityBase;

        IQueryable<T> GetQuery<T>() where T : class;

        Task<int> SaveAsync(bool allowHardDelete = false);

        int Save(bool allowHardDelete = false);

        DbSet<T> GetDbSet<T>() where T : class;

        void Rollback();
    }
}
