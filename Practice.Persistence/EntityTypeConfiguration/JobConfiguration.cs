using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Practice.Domain.Models;

namespace Practice.Persistence.EntityTypeConfiguration
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.HasKey(j => j.JobId);
            builder.Property(j => j.JobId).HasColumnName("Job_Id");
            builder.Property(j => j.JobTitle).HasColumnName("Job_Title");
            builder.Property(j => j.MinSalary).HasColumnName("Min_Salary");
            builder.Property(j => j.MaxSalary).HasColumnName("Max_Salary");
        }
    }
}
