using Microsoft.EntityFrameworkCore;

namespace P3.Models.EFModels
{
    public class P3Context: DbContext
    {
        public P3Context(DbContextOptions<P3Context> options): base(options)
        {

        }

        public DbSet<Folder> Folders { get; set; }
        public DbSet<File> Files { get; set; }

        public int SaveEntityChanges(bool allowHardDelete = false)
        {
            AddAuditInfo(allowHardDelete);
            return base.SaveChanges();
        }

        public async Task<int> SaveEntityChangesAsync(bool allowHardDelete = false)
        {
            AddAuditInfo(allowHardDelete);
            return await base.SaveChangesAsync();
        }

        private void AddAuditInfo(bool allowHardDelete = false)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(x => x.Entity is EntityBase);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Deleted && !allowHardDelete)
                {
                    ((EntityBase)entry.Entity).IsActive = false;
                    entry.State = EntityState.Modified;
                }

                if (entry.State == EntityState.Added)
                {
                    ((EntityBase)entry.Entity).IsActive = true;
                    ((EntityBase)entry.Entity).CreationDate = DateTime.UtcNow;
                }
                else
                {
                    entry.Property(nameof(EntityBase.CreationDate)).IsModified = false;
                }

                ((EntityBase)entry.Entity).LastModifiedDate = DateTime.UtcNow;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>().HasOne(f => f.ParentFolder).WithMany(f => f.ChildrenFolders).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
