using System.ComponentModel.DataAnnotations;

namespace Practice.Domain.Models
{
    public class JobHistory
    {
        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(10)]
        public string JobId { get; set; }

        public int? DepartmentId { get; set; }

    }
}
