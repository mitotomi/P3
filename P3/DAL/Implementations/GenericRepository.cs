using Microsoft.EntityFrameworkCore;
using P3.DAL.Contracts;
using P3.Models.EFModels;
using System.Linq.Expressions;

namespace P3.DAL.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {
        private readonly P3Context context;

        public GenericRepository(P3Context context)
        {
            this.context = context;
        }

        public IQueryable<T> Query()
        {
            return context.Set<T>().AsQueryable();
        }

        public IQueryable<T> ReadQuery()
        {
            return context.Set<T>().AsNoTracking().AsQueryable();
        }

        public IQueryable<T> ReadActiveQuery()
        {
            var query = context.Set<T>().AsNoTracking().AsQueryable();

            if (typeof(EntityBase).IsAssignableFrom(typeof(T)))
            {
                query = query.Where(q => q.IsActive);
            }

            return query;
        }

        public ICollection<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public T GetByUniqueId(string id)
        {
            return context.Set<T>().Find(id);
        }

        public T GetByUniqueId(int id)
        {
            return context.Set<T>().Find(id);
        }

        public async Task<T> GetByUniqueIdAsync(string id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().SingleOrDefault(match);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return context.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await context.Set<T>().Where(match).ToListAsync();
        }

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        public void Update(T updated)
        {
            if (updated == null)
            {
                return;
            }

            context.Set<T>().Update(updated);
        }

        public void Delete(T t)
        {
            context.Set<T>().Remove(t);
        }

        public void DeleteRange(IEnumerable<T> t)
        {
            context.Set<T>().RemoveRange(t);
        }

        public void Delete(int id)
        {
            var entity = GetByUniqueId(id);
            Delete(entity);
        }

        public int Count()
        {
            return context.Set<T>().Count();
        }

        public async Task<int> CountAsync()
        {
            return await context.Set<T>().CountAsync();
        }

        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", int? page = null,
            int? pageSize = null)
        {
            IQueryable<T> query = context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (includeProperties != null)
            {
                foreach (
                    var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query.ToList();
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().Where(predicate);
        }

        public bool Exist(Expression<Func<T, bool>> predicate)
        {
            var exist = context.Set<T>().Where(predicate);
            return exist.Any() ? true : false;
        }
    }
}
