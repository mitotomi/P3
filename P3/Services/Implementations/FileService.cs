using AutoMapper;
using P3.DAL.Contracts;
using P3.Filtering.Providers;
using P3.Models.Filtering;
using P3.Models.Requests;
using P3.Models.ViewModels;
using P3.Services.Contracts;
using System.Transactions;
using File = P3.Models.EFModels.File;

namespace P3.Services.Implementations
{
    public class FileService : IFileService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly FileFilterProvider filterProvider;

        public FileService(IUnitOfWork unitOfWork, IMapper mapper, FileFilterProvider filterProvider)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.filterProvider = filterProvider;
        }

        #region Readers

        public List<FileViewModel> GetAll()
        {
            try
            {
                var query = unitOfWork.GetGenericRepository<File>().ReadActiveQuery();

                var files = mapper.ProjectTo<FileViewModel>(query).ToList();

                return files;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<FileViewModel> Search(FilterRequest request)
        {
            try
            {
                var query = unitOfWork.GetGenericRepository<File>().ReadActiveQuery();

                var queryFilter = filterProvider.GetFilters(request);
                foreach(var filter in queryFilter)
                {
                    query = filter.FilterQuery(query);
                }

                query = query.Take(10);

                var results = mapper.ProjectTo<FileViewModel>(query).ToList();
                return results;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public FileDetailViewModel GetDetailed(long id)
        {
            try
            {
                var query = unitOfWork.GetGenericRepository<File>().ReadActiveQuery().Where(f => f.Id == id);
                var file = mapper.ProjectTo<FileDetailViewModel>(query).FirstOrDefault();
                if (file == null)
                {
                    throw new ArgumentException();
                }
                return file;

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        #endregion

        #region Writers

        public async Task<FileDetailViewModel> Create(CreateFileRequest request)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var entityToInsert = mapper.Map<File>(request);

                    unitOfWork.GetGenericRepository<File>().Add(entityToInsert);

                    await unitOfWork.SaveAsync();

                    var createdFile = GetDetailed(entityToInsert.Id);

                    scope.Complete();
                    return createdFile;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Delete(long id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var existingEntity = unitOfWork.GetGenericRepository<File>().Find(f => f.IsActive && f.Id == id);

                if (existingEntity != null)
                {
                    throw new ArgumentException();
                }

                unitOfWork.GetGenericRepository<File>().Delete(existingEntity);

                unitOfWork.Save();
                scope.Complete();
            }
        }

        #endregion
    }
}
