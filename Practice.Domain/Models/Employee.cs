using System.ComponentModel.DataAnnotations;

namespace Practice.Domain.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(25)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string Email { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        [StringLength(10)]
        public string JobId { get; set; }

        public decimal? Salary { get; set; }

        public decimal? CommissionPct { get; set; }

        public int? ManagerId { get; set; }

        public int? DepartmentId { get; set; }
    }
}
