using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class PaymentType
    {
        public int PaymentTypeId { get; set; }

        [Display(Name = "Тип оплаты")]
        [MaxLength(100)]
        public string PaymentTypeName { get; set; }
    }
}
