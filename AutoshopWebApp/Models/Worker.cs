using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class Worker
    {
        public int WorkerId { get; set; }

        [Display(Name = "Имя")]
        [MaxLength(100)]
        public string Firstname { get; set; }

        [Display(Name = "Фамилия")]
        [MaxLength(100)]
        public string Lastname { get; set; }

        [Display(Name = "Отчество")]
        [MaxLength(100)]
        public string Patronymic { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        [Display(Name = "ID улицы")]
        public int StreetId { get; set; }

        [Display(Name = "Номер дома")]
        public int HouseNumber { get; set; }

        [Display(Name = "Номер квартиры")]
        public int ApartmentNumber { get; set; }

        [Display(Name = "ID должности")]
        public int PositionId { get; set; }
    }
}
