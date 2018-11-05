using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class ClientSeller
    {
        public int ClientSellerId { get; set; }

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

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        [Required]
        [Display(Name = "Номер паспорта")]
        [MaxLength(50)]
        public string PasNumber { get; set; }

        [Display(Name = "Дата выдачи пасп.")]
        [DataType(DataType.Date)]
        public DateTime PasIssueDate { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Паспорт выдан")]
        public string PasIssueBy { get; set; }

        [Display(Name = "ID улицы")]
        public int StreetId { get; set; }

        [Display(Name = "Номер дома")]
        public int HouseNumber { get; set; }

        [Display(Name = "Номер квартиры")]
        public int ApartmentNumber { get; set; }

        [Display(Name = "ID автомобиля")]
        public int CarId { get; set; }

        [Display(Name = "Дата продажи")]
        [DataType(DataType.Date)]
        public DateTime SellingDate { get; set; }

        [Required]
        [Display(Name = "Номер документа")]
        [MaxLength(50)]
        public string DocNumber { get; set; }

        [Required]
        [Display (Name = "Название документа")]
        [MaxLength(100)]
        public string DocName { get; set; }

        [Display(Name = "Дата выдачи документа")]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [Required]
        [Display(Name = "Документ выдан")]
        [MaxLength(100)]
        public string OwnDocIssuedBy { get; set; }

        public int WorkerId { get; set; }
    }
}
