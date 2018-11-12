using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models.ForShow
{
    public class IncomeStatement
    {
        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0: MM.yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Убыток")]
        [DataType(DataType.Currency)]
        public decimal Loss { get; set; }

        [Display(Name = "Доход")]
        [DataType(DataType.Currency)]
        public decimal Profit { get; set; }

        [Display(Name = "Прибыль")]
        [DataType(DataType.Currency)]
        public decimal Sum { get; set; }
    }
}
