using P3.Filtering.Filters.FileFilters;
using P3.Models.Filtering;
using File = P3.Models.EFModels.File;

namespace P3.Filtering.Providers
{
    public class FileFilterProvider
    {
        public FileFilterProvider()
        {
        }

        public IEnumerable<Filter<File>> GetFilters(FilterRequest filterRequest)
        {
            List<Filter<File>> filters = new List<Filter<File>>();

            if (filterRequest == null)
            {
                return filters;
            }

            if (filterRequest.Name != null)
            {
                filters.Add(new FileNameFilter(filterRequest.Name));
            }

            if (filterRequest.FolderId != null)
            {
                filters.Add(new FileFolderFilter(filterRequest.FolderId.Value));
            }

            return filters;
        }
    }
}
