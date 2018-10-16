using AutoshopWebApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class Position
    {
        public int PositionId { get; set; }

        [Display(Name = "Должность")]
        [MaxLength(100)]
        public string PositionName { get; set; }

        [Display(Name = "Оклад")]
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }


        public static async Task<IList<SelectListItem>> GetSelectListItems(ApplicationDbContext context)
        {
            return await
                (from pos in context.Positions
                 select new SelectListItem
                 {
                     Value = pos.PositionId.ToString(),
                     Text = pos.PositionName
                 }).ToListAsync();
        }
    }
}
