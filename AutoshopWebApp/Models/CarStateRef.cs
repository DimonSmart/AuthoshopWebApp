using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class CarStateRef
    {
        public int CarStateRefId { get; set; }

        public Car Car { get; set; }
        public int CarId { get; set; }

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
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(16,2)")]
        public decimal ExpertisePrice { get; set; }
    }
}
