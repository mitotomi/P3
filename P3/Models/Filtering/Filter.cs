namespace P3.Models.Filtering
{
    public abstract class Filter<T> where T : class
    {
        public abstract IQueryable<T> FilterQuery(IQueryable<T> source);
    }
}
