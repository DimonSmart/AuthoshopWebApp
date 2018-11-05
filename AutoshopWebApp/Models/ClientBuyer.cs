using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class ClientBuyer
    {
        public int ClientBuyerId { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Имя")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Отчество")]
        [MaxLength(100)]
        public string Patronymic { get; set; }

        [Required]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        [Required]
        [Display(Name = "Номер паспорта")]
        [MaxLength(50)]
        public string PasNumber { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Паспорт выдан")]
        public string PasIssueBy { get; set; }

        [Display(Name = "Дата выдачи пасп.")]
        [DataType(DataType.Date)]
        public DateTime PasIssueDate { get; set; }

        [Display(Name = "ID улицы")]
        public int StreetId { get; set; }

        [Display(Name = "Номер дома")]
        public int HouseNumber { get; set; }

        [Display(Name = "Номер квартиры")]
        public int ApartmentNumber { get; set; }

        [Display(Name = "ID автомобиля")]
        public int CarId { get; set; }

        [Display(Name = "Дата покупки")]
        [DataType(DataType.Date)]
        public DateTime BuyDate { get; set; }

        [Display(Name = "Номер счёта")]
        [MaxLength(50)]
        public string AccountNumber { get; set; }

        [Display(Name = "Тип оплаты")]
        public int PaymentTypeId { get; set; }

        public int WorkerId { get; set; }
    }
}
