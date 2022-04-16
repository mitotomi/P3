using AutoMapper;
using P3.DAL.Contracts;
using P3.Models.EFModels;
using P3.Models.Requests;
using P3.Models.ViewModels;
using P3.Services.Contracts;
using System.Transactions;

namespace P3.Services.Implementations
{
    public class FolderService : IFolderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public FolderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        #region Readers
        public List<FolderViewModel> GetAll()
        {
            var query = unitOfWork.GetGenericRepository<Folder>().ReadActiveQuery();

            var folders = mapper.ProjectTo<FolderViewModel>(query).ToList();

            return folders;
        }

        public FolderDetailViewModel GetDetailed(long id)
        {
            var query = unitOfWork.GetGenericRepository<Folder>().ReadActiveQuery().Where(f => f.Id == id);
            var folder = mapper.ProjectTo<FolderDetailViewModel>(query).FirstOrDefault();
            if (folder == null)
            {
                throw new ArgumentException();
            }
            return folder;
        }

        #endregion

        #region Writers

        public async Task<FolderDetailViewModel> Create(CreateFolderRequest request)
        {
            try
            {
                using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var entityToInsert = mapper.Map<Folder>(request);

                    unitOfWork.GetGenericRepository<Folder>().Add(entityToInsert);

                    await unitOfWork.SaveAsync();

                    var createdFolder = GetDetailed(entityToInsert.Id);

                    scope.Complete();
                    return createdFolder;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(long id)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {

                var existingEntity = unitOfWork.GetGenericRepository<Folder>().Find(f => f.IsActive && f.Id == id);

                if (existingEntity != null)
                {
                    throw new ArgumentException();
                }

                unitOfWork.GetGenericRepository<Folder>().Delete(existingEntity);

                unitOfWork.Save();
                scope.Complete();
            }
        }

        #endregion
    }
}
