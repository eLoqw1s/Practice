using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Domain.Models;

namespace Practice.Persistence.EntityTypeConfiguration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.EmployeeId);
            builder.Property(e => e.EmployeeId).HasColumnName("Employee_Id");
            builder.Property(e => e.FirstName).HasColumnName("First_Name");
            builder.Property(e => e.LastName).HasColumnName("Last_Name");
            builder.Property(e => e.PhoneNumber).HasColumnName("Phone_Number");
            builder.Property(e => e.HireDate).HasColumnName("Hire_Date");
            builder.Property(e => e.JobId).HasColumnName("Job_Id");
            builder.Property(e => e.CommissionPct).HasColumnName("Commission_Pct");
            builder.Property(e => e.ManagerId).HasColumnName("Manager_Id");
            builder.Property(e => e.DepartmentId).HasColumnName("Department_Id");
        }
    }
}
