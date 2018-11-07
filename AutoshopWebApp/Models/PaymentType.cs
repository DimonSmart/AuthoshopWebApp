using AutoshopWebApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public static async Task<List<SelectListItem>> GetSelectList(ApplicationDbContext _context)
        {
            return await
                (from val in _context.PaymentTypes
                 select new SelectListItem
                 {
                     Value = val.PaymentTypeId.ToString(),
                     Text = val.PaymentTypeName
                 }).ToListAsync();
        }
    }
}
