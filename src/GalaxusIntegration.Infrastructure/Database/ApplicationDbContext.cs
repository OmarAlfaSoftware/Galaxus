using Microsoft.EntityFrameworkCore;

namespace GalaxusIntegration.Infrastructure.Database;

public class ApplicationDbContext :DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    // Define DbSets for your entities here
    // public DbSet<YourEntity> YourEntities { get; set; }

}