using Microsoft.AspNetCore.Mvc;
using P3.Models.Requests;
using P3.Models.ViewModels;

namespace P3.Services.Contracts
{
    public interface IFolderService
    {
        List<FolderViewModel> GetAll();
        Task<FolderDetailViewModel> Create(CreateFolderRequest request);
        void Delete(long id);
    }
}
