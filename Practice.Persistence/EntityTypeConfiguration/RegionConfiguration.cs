using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Domain.Models;

namespace Practice.Persistence.EntityTypeConfiguration
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasKey(r => r.RegionId);
            builder.Property(r => r.RegionId).HasColumnName("Region_Id");
            builder.Property(r => r.RegionName).HasColumnName("Region_Name");
        }
    }
}
