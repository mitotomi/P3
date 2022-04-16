namespace P3.Models.ViewModels
{
    public class FolderDetailViewModel: FolderViewModel
    {
        public FolderDetailViewModel ParentFolder { get; set; }

        public List<FolderDetailViewModel> ChildrenFolders { get; set; }
        public List<FileViewModel> ChildrenFiles { get; set; }

    }
}
