using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class Car
    {
        public int CarId { get; set; }

        [Display(Name = "ID модели")]
        public int MarkAndModelID { get; set; }

        [Display(Name = "Цвет")]
        public string Color { get; set; }

        [Display(Name = "Номер двигателя")]
        [MaxLength(100)]
        public string EngineNumber { get; set; }

        [Display(Name = "Номер регистрации")]
        [MaxLength(100)]
        public string RegNumber { get; set; }

        [Display(Name = "Номер кузова")]
        [MaxLength(100)]
        public string BodyNumber { get; set; }

        [Display(Name = "Номер шасси")]
        [MaxLength(100)]
        public string ChassisNumber { get; set; }

        [Display(Name = "Дата выпуска")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Пробег")]
        public int Run { get; set; }

        [Display(Name = "Первоначальная стоимость")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ReleasePrice { get; set; }

        [Display(Name = "Стоимость продажи")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? SellingPrice { get; set; }

        [Display(Name = "Стоимость покупки")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? BuyingPrice { get; set; }

        [Display(Name = "ID справки")]
        public int? CarReferenceId { get; set; }
    }
}
