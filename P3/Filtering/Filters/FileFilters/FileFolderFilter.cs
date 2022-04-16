using P3.Models.Filtering;
using File = P3.Models.EFModels.File;

namespace P3.Filtering.Filters.FileFilters
{
    public class FileFolderFilter : Filter<File>
    {
        private readonly long id;

        public FileFolderFilter(long id)
        {
            this.id = id;
        }

        public override IQueryable<File> FilterQuery(IQueryable<File> source)
        {
            return source.Where(f => f.FolderId == id);
        }
    }
}
