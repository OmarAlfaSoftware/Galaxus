using Microsoft.EntityFrameworkCore;

namespace GalaxusIntegration.Infrastructure.Data
{
    public class GalaxusDbContext : DbContext
    {
        public GalaxusDbContext(DbContextOptions<GalaxusDbContext> options) : base(options) { }
        // DbSets for Product, Order, FileLog, etc.
    }
}