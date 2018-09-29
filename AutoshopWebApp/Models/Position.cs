using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class Position
    {
        public int PositionId { get; set; }

        [Display(Name = "Должность")]
        [MaxLength(100)]
        public string PositionName { get; set; }

        [Display(Name = "Оклад")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }
    }
}
