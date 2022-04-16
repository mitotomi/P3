using P3.Models.Filtering;
using File = P3.Models.EFModels.File;

namespace P3.Filtering.Filters.FileFilters
{
    public class FileNameFilter : Filter<File>
    {
        private readonly string searchString;

        public FileNameFilter(string searchString)
        {
            this.searchString = searchString;
        }

        public override IQueryable<File> FilterQuery(IQueryable<File> source)
        {
            return source.Where(f => f.Name.ToLower().StartsWith(searchString.ToLower()));
        }
    }
}
