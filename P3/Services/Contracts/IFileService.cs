using P3.Models.Filtering;
using P3.Models.Requests;
using P3.Models.ViewModels;

namespace P3.Services.Contracts
{
    public interface IFileService
    {
        List<FileViewModel> GetAll();
        List<FileViewModel> Search(FilterRequest request);
        Task<FileDetailViewModel> Create(CreateFileRequest request);
        void Delete(long id);

    }
}
