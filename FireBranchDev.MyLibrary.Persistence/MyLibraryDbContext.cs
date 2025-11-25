using FireBranchDev.MyLibrary.Domain.Common;
using FireBranchDev.MyLibrary.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FireBranchDev.MyLibrary.Persistence;

public class MyLibraryDbContext(DbContextOptions<MyLibraryDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MyLibraryDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<Base>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    DateTime now = DateTime.Now;
                    entry.Entity.Created = now;
                    entry.Entity.Updated = now;
                    break;
                case EntityState.Modified:
                    entry.Entity.Updated = DateTime.Now;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
