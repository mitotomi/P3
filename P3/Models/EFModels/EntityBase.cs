namespace P3.Models.EFModels
{
    public class EntityBase
    {
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
