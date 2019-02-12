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

        public Worker Worker { get; set; }
        public int WorkerId { get; set; }

        public Position Position { get; set; }
        public int PositionId { get; set; }

        [Display(Name = "Причина перевода")]
        [MaxLength(200)]
        [DataType(DataType.MultilineText)]
        public string Reason { get; set; }

        [Display(Name = "Номер приказа")]
        [MaxLength(50)]
        [Required]
        public string OrderNumber { get; set; }

        [Display(Name = "Дата приказа")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
    }
}
