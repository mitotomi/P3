namespace P3.Models.EFModels
{
    public class Folder: EntityBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long? ParentFolderId { get; set; }
        public Folder ParentFolder { get; set; }

        public List<Folder> ChildrenFolders { get; set; }
        public List<File> ChildrenFiles { get; set; }
    }
}
