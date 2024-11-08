using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Domain.Models;

namespace Practice.Persistence.EntityTypeConfiguration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.CountryId);
            builder.Property(c => c.CountryId).HasColumnName("Country_Id");
            builder.Property(c => c.CountryName).HasColumnName("Country_Name");
            builder.Property(c => c.RegionId).HasColumnName("Region_Id");
        }
    }
}
