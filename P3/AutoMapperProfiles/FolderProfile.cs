using AutoMapper;
using P3.Models.EFModels;
using P3.Models.Requests;
using P3.Models.ViewModels;

namespace P3.AutoMapperProfiles
{
    public class FolderProfile : Profile
    {
        public FolderProfile()
        {
            CreateMap<Folder, FolderViewModel>();

            CreateMap<CreateFolderRequest, Folder>();

            CreateMap<Folder, FolderDetailViewModel>()
                .IncludeBase<Folder, FolderViewModel>();
        }
    }
}
