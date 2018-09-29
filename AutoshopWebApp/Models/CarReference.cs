using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class CarReference
    {
        public int CarReferenceId { get; set; }

        [Display(Name = "Номер справки")]
        [MaxLength(100)]
        public string ReferenceNumber { get; set; }

        [Display(Name = "Дата справки")]
        [DataType(DataType.Date)]
        public DateTime ReferenceDate { get; set; }

        [Display(Name = "Эксперт")]
        [MaxLength(100)]
        public string Expert { get; set; }

        [Display(Name = "Стоимость экспертизы")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal ExpertisePrice { get; set; }
    }
}
