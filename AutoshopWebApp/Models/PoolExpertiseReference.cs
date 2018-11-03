using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class PoolExpertiseReference
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Имя клиента")]
        public string ClientFirstname { get; set; }

        [Required]
        [Display(Name = "Фамилия клиента")]
        public string ClientLastname { get; set; }

        [Required]
        [Display(Name = "Отчество клиента")]
        public string ClientPatronymic { get; set; }

        public int CarId { get; set; }

        public int WorkerId { get; set; }

        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }
    }
}
