namespace P3.Models.EFModels
{
    public class File: EntityBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long FolderId { get; set; }
        public Folder Folder { get; set; }
    }
}
