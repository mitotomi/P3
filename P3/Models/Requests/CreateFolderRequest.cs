namespace P3.Models.Requests
{
    public class CreateFolderRequest
    {
        public string Name { get; set; }
        public long? ParentFolderId { get; set; }
    }
}
