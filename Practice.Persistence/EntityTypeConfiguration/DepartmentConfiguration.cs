using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Domain.Models;

namespace Practice.Persistence.EntityTypeConfiguration
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(d => d.DepartmentId);
            builder.Property(d => d.DepartmentId).HasColumnName("Department_Id");
            builder.Property(d => d.DepartmentName).HasColumnName("Department_Name");
            builder.Property(d => d.ManagerId).HasColumnName("Manager_Id");
            builder.Property(d => d.LocationId).HasColumnName("Location_Id");
        }
    }
}
