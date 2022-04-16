using P3.Models.EFModels;

namespace P3.DAL.Implementations
{
    class RepositoryFactory
    {
        private readonly IDictionary<Type, Func<P3Context, object>> _factoryCache;

        public RepositoryFactory()
        {
            _factoryCache = GetFactories();
        }

        public Func<P3Context, object> GetRepositoryFactoryForEntityType<T>() where T : EntityBase
        {
            Func<P3Context, object> factory = GetRepositoryFactoryFromCache<T>();
            if (factory != null)
            {
                return factory;
            }

            return DefaultEntityRepositoryFactory<T>();
        }

        public Func<P3Context, object> GetRepositoryFactoryFromCache<T>()
        {
            _factoryCache.TryGetValue(typeof(T), out Func<P3Context, object> factory);
            return factory;
        }

        private IDictionary<Type, Func<P3Context, object>> GetFactories()
        {
            Dictionary<Type, Func<P3Context, object>> dic = new Dictionary<Type, Func<P3Context, object>>
            {
            };
            //Add Extended and Custom Repositories here
            return dic;
        }

        private Func<P3Context, object> DefaultEntityRepositoryFactory<T>() where T : EntityBase
        {
            return dbContext => new GenericRepository<T>(dbContext);
        }
    }
}
