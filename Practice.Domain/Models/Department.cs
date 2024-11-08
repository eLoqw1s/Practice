using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Practice.Domain.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(30)]
        public string DepartmentName { get; set; }

        public int? ManagerId { get; set; }

        public int? LocationId { get; set; }

    }
}
