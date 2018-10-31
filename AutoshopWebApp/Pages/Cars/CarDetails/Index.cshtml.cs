using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using AutoshopWebApp.Models.ForShow;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class IndexModel : PageModel
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public IndexModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public OutputCarModel CarData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carQuery =
                from car in _context.Cars
                where id == car.CarId
                join mark in _context.MarkAndModels on car.MarkAndModelID equals mark.MarkAndModelId
                select new OutputCarModel
                {
                    Car = car,
                    MarkAndModel = mark,
                };


            CarData = await carQuery.AsNoTracking().FirstOrDefaultAsync();

            if (CarData == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
