using System.ComponentModel.DataAnnotations;

namespace Practice.Domain.Models
{
    public class Job
    {
        [StringLength(10)]
        public string JobId { get; set; }

        [Required]
        [StringLength(35)]
        public string JobTitle { get; set; }

        public int? MinSalary { get; set; }

        public int? MaxSalary { get; set; }

    }
}
