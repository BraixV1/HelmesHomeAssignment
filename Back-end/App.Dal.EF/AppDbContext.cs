using App.Domain;
using Microsoft.EntityFrameworkCore;

namespace App.Dal.EF;

public class AppDbContext : DbContext
{
    public DbSet<ToDo> Todos { get; set; } = default!;
    
    
    
    public AppDbContext(DbContextOptions options) : base(options) {}
    
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entity in ChangeTracker.Entries().Where(e => e.State != EntityState.Deleted))
        {
            foreach (var prop in entity
                         .Properties
                         .Where(x => x.Metadata.ClrType == typeof(DateTime)))
            {
                Console.WriteLine(prop);
                prop.CurrentValue = ((DateTime) prop.CurrentValue).ToUniversalTime();
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    
    
}