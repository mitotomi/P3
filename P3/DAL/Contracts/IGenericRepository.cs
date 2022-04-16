using P3.Models.EFModels;
using System.Linq.Expressions;

namespace P3.DAL.Contracts
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        IQueryable<T> Query();

        IQueryable<T> ReadQuery();

        IQueryable<T> ReadActiveQuery();

        /// <summary>
        /// Don't use this. Extremely expensive
        /// </summary>
        /// <returns></returns>
        ICollection<T> GetAll();

        /// <summary>
        /// Don't use this. Extremely expensive
        /// </summary>
        /// <returns></returns>
        Task<ICollection<T>> GetAllAsync();

        T GetById(int id);

        Task<T> GetByIdAsync(int id);

        T GetByUniqueId(string id);

        T GetByUniqueId(int id);

        Task<T> GetByUniqueIdAsync(string id);

        T Find(Expression<Func<T, bool>> match);

        Task<T> FindAsync(Expression<Func<T, bool>> match);

        ICollection<T> FindAll(Expression<Func<T, bool>> match);

        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);

        void Add(T entity);

        //Task<T> AddAsync(T entity);

        void AddRange(IEnumerable<T> entities);

        void Update(T updated);

        //Task<T> UpdateAsync(T updated);

        void Delete(T t);

        //Task<int> DeleteAsync(T t);

        void DeleteRange(IEnumerable<T> t);

        void Delete(int id);

        int Count();

        Task<int> CountAsync();

        IEnumerable<T> Filter(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "",
            int? page = null,
            int? pageSize = null);

        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);

        bool Exist(Expression<Func<T, bool>> predicate);

    }
}
