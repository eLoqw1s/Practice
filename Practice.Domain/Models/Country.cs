using System.ComponentModel.DataAnnotations;

namespace Practice.Domain.Models
{
    public class Country
    {
        [StringLength(2)]
        public string CountryId { get; set; }

        [StringLength(40)]
        public string CountryName { get; set; }
        public int? RegionId { get; set; }

    }
}
