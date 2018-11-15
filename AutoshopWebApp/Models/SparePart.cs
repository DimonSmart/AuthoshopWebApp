using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class SparePart
    {
        public int SparePartId { get; set; }

        public int MarkAndModelId { get; set; }

        [Display(Name = "Стоимость запчасти")]
        [Column(TypeName = "decimal(18,2)")]
        [DataType(DataType.Currency)]
        public decimal PartPrice { get; set; }

        [Display(Name = "Количество на складе")]
        public int PartCount { get; set; }

        [Required]
        [Display(Name = "Название запчасти")]
        [MaxLength(100)]
        public string PartName { get; set; }
    }
}
