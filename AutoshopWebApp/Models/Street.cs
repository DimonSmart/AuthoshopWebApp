using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class Street
    {
        public int StreetId { get; set; }

        [Display(Name = "Название улицы")]
        [MaxLength(100)]
        public string StreetName { get; set; }
    }
}
