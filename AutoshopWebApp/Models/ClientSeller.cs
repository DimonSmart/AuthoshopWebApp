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

        [Display(Name = "Имя")]
        [MaxLength(100)]
        public string Firstname { get; set; }

        [Display(Name = "Фамилия")]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Display(Name = "Отчество")]
        [MaxLength(100)]
        public string Patronymic { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        [Display(Name = "Номер паспорта")]
        [MaxLength(50)]
        public string PasNumber { get; set; }

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

        [Display(Name = "Номер документа")]
        [MaxLength(50)]
        public string DocNumber { get; set; }

        [Display (Name = "Название документа")]
        [MaxLength(100)]
        public string DocName { get; set; }

        [Display(Name = "Дата выдачи документа")]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [Display(Name = "Документ выдан")]
        [MaxLength(100)]
        public string IssuedBy { get; set; }
    }
}
