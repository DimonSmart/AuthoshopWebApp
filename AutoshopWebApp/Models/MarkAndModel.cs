using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class MarkAndModel
    {
        public int MarkAndModelId { get; set; }

        [Display(Name = "Марка")]
        [MaxLength(100)]
        [Required]
        public string CarMark { get; set; }

        [Display(Name = "Модель")]
        [MaxLength(100)]
        [Required]
        public string CarModel { get; set; }

        public List<Car> Cars { get; set; }

        public MarkAndModel()
        {
            Cars = new List<Car>();
        }
    }
}
