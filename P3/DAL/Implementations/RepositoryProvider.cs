using P3.DAL.Contracts;
using P3.Models.EFModels;

namespace P3.DAL.Implementations
{
    class RepositoryProvider : IRepository
    {
        public P3Context DbContext { get; set; }

        private readonly RepositoryFactory factory;

        protected Dictionary<Type, object> Repositories { get; private set; }

        public RepositoryProvider()
        {
            factory = new RepositoryFactory();
            Repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<T> GetGenericRepository<T>() where T : EntityBase
        {
            Func<P3Context, object> repositoryFactoryForEntityTypeDelegate = factory.GetRepositoryFactoryForEntityType<T>();
            return GetCustomRepository<IGenericRepository<T>>(repositoryFactoryForEntityTypeDelegate);
        }

        public virtual T GetCustomRepository<T>(Func<P3Context, object> factory = null) where T : class
        {
            Repositories.TryGetValue(typeof(T), out object repository);
            if (repository != null)
            {
                return (T)repository;
            }
            return CreateRepository<T>(factory, DbContext);
        }

        private T CreateRepository<T>(Func<P3Context, object> factory, P3Context dbContext)
        {
            Func<P3Context, object> repositoryFactory;

            if (factory != null)
            {
                repositoryFactory = factory;
            }
            else
            {
                repositoryFactory = this.factory.GetRepositoryFactoryFromCache<T>();
            }
            if (repositoryFactory == null)
            {
                throw new NotSupportedException(typeof(T).FullName);
            }

            T repository = (T)repositoryFactory(dbContext);
            Repositories[typeof(T)] = repository;
            return repository;
        }
    }
}
