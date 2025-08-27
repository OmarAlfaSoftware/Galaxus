using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GalaxusIntegration.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<object> // Replace 'object' with your Order entity
    {
        public void Configure(EntityTypeBuilder<object> builder)
        {
            // Configure Order entity
        }
    }
}