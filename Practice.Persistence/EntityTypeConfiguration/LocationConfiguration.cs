using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Domain.Models;

namespace Practice.Persistence.EntityTypeConfiguration
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(l => l.LocationId);
            builder.Property(l => l.LocationId).HasColumnName("Location_Id");
            builder.Property(l => l.StreetAddress).HasColumnName("Street_Address");
            builder.Property(l => l.PostalCode).HasColumnName("Postal_Code");
            builder.Property(l => l.StateProvince).HasColumnName("State_Province");
            builder.Property(l => l.CountryId).HasColumnName("Country_Id");
        }
    }
}
