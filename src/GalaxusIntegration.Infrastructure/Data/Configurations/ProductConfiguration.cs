using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GalaxusIntegration.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<object> // Replace 'object' with your Product entity
    {
        public void Configure(EntityTypeBuilder<object> builder)
        {
            // Configure Product entity
        }
    }
}