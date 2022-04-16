namespace P3.Models.ViewModels
{
    public class FolderViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentFolderId { get; set; }
    }
}
