using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Practice.Domain.Models;

namespace Practice.Persistence.EntityTypeConfiguration
{
    public class JobHistoryConfiguration : IEntityTypeConfiguration<JobHistory>
    {
        public void Configure(EntityTypeBuilder<JobHistory> builder)
        {
            builder.HasKey(jh => new { jh.EmployeeId, jh.StartDate });
            builder.Property(jh => jh.EmployeeId).HasColumnName("Employee_Id");
            builder.Property(jh => jh.StartDate).HasColumnName("Start_Date");
            builder.Property(jh => jh.EndDate).HasColumnName("End_Date");
            builder.Property(jh => jh.JobId).HasColumnName("Job_Id");
            builder.Property(jh => jh.DepartmentId).HasColumnName("Department_Id");
        }
    }
}
