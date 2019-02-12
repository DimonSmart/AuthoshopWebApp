using Microsoft.AspNetCore.Mvc.Rendering;
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

       
        [Required]
        public MarkAndModel MarkAndModel { get; set; }
        public int MarkAndModelId { get; set; }

        [Required]
        [Display(Name = "Цвет")]
        public string Color { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Номер двигателя")]
        public string EngineNumber { get; set; }
        
        [Required]
        [Display(Name = "Номер кузова")]
        [MaxLength(100)]
        public string BodyNumber { get; set; }

        [Display(Name = "Номер шасси")]
        [MaxLength(100)]
        public string ChassisNumber { get; set; }

        [Required]
        [Display(Name = "Дата выпуска")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Номер регистрации")]
        [MaxLength(10)]
        public string RegNumber { get; set; }


        [Display(Name = "Пробег, км")]
        public int Run { get; set; }

        [Display(Name = "Первоначальная стоимость, руб.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(16,2)")]
        public decimal? ReleasePrice { get; set; }

        [Display(Name = "Стоимость продажи, руб.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(16,2)")]
        public decimal? SellingPrice { get; set; }

        [Display(Name = "Стоимость покупки, руб.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(16,2)")]
        public decimal? BuyingPrice { get; set; }
    }
}
