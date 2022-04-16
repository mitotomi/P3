using AutoMapper;
using P3.Models.Requests;
using P3.Models.ViewModels;
using File = P3.Models.EFModels.File;

namespace P3.AutoMapperProfiles
{
    public class FileProfile: Profile
    {
        public FileProfile()
        {
            CreateMap<File, FileViewModel>();

            CreateMap<File, FileDetailViewModel>();

            CreateMap<CreateFileRequest, File>();
        }
    }
}
