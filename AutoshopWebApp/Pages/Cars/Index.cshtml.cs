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

namespace AutoshopWebApp.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public IndexModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<OutputCarModel> OutputCarModels { get;set; }

        public async Task<IActionResult> OnGetAsync(string search, SaleStatus? saleStatus)
        {
            var query =
                from car in _context.Cars
                join mark in _context.MarkAndModels
                on car.MarkAndModelID equals mark.MarkAndModelId
                select new OutputCarModel
                {
                    Car = new Car
                    {
                        CarId = car.CarId,
                        Color = car.Color,
                        EngineNumber = car.EngineNumber,
                        RegNumber = car.RegNumber,
                        BodyNumber = car.BodyNumber,
                        ReleaseDate = car.ReleaseDate,
                        Run = car.Run,
                        SaleStatus = car.SaleStatus,
                    },
                    MarkAndModel = new MarkAndModel
                    {
                        CarMark = mark.CarMark,
                        CarModel = mark.CarModel
                    }
                };

            if(!String.IsNullOrEmpty(search))
            {
                var substrings = search.Split(' ');

                foreach (var str in substrings)
                {
                    query =
                        from carData in query
                        where carData.MarkAndModel.CarMark.Contains(str, StringComparison.OrdinalIgnoreCase) ||
                        carData.MarkAndModel.CarModel.Contains(str, StringComparison.OrdinalIgnoreCase) ||
                        carData.Car.Color.Contains(str, StringComparison.OrdinalIgnoreCase)
                        select carData;
                }
            }

            OutputCarModels = await query.AsNoTracking().ToListAsync();

            return Page();
        }
    }
}
