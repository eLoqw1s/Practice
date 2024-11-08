﻿using System.ComponentModel.DataAnnotations;

namespace Practice.Domain.Models
{
    public class Region
    {
        public int RegionId { get; set; }

        [StringLength(25)]
        public string RegionName { get; set; }

    }
}
