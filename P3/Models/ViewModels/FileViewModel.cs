namespace P3.Models.ViewModels
{
    public class FileViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long FolderId { get; set; }
        public FolderViewModel Folder { get; set; }
    }
}
