using P3.Models.EFModels;

namespace P3.DAL.Contracts
{
    public interface IRepository
    {
        P3Context DbContext { get; set; }

        IGenericRepository<T> GetGenericRepository<T>() where T : EntityBase;

    }
}
