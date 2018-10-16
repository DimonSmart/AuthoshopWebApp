using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class TransactionOrder
    {
        public int TransactionOrderId { get; set; }

        [Display(Name = "ID работника")]
        public int WorkerId { get; set; }

        [Display(Name = "ID должности")]
        public int PositionId { get; set; }

        [Display(Name = "Причина перевода")]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }

        [Display(Name = "Номер приказа")]
        [MaxLength(50)]
        public string OrderNumber { get; set; }

        [Display(Name = "Дата приказа")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
    }
}
