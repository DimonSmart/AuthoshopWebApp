using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using AutoshopWebApp.Models.ForShow;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public CreateModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            CarStatusSelectList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = SaleStatus.Expertise.ToString(),
                    Text = SaleStatus.Expertise.GetName(),
                },
                new SelectListItem
                {
                    Value = SaleStatus.ForBuy.ToString(),
                    Text = SaleStatus.ForBuy.GetName(),
                },
                new SelectListItem
                {
                    Value = SaleStatus.ForSold.ToString(),
                    Text = SaleStatus.ForSold.GetName(),
                },
                new SelectListItem
                {
                    Value = SaleStatus.Sold.ToString(),
                    Text = SaleStatus.Sold.GetName()
                },
            };
            return Page();
        }

        [BindProperty]
        public Car CarData { get; set; }

        [BindProperty]
        public MarkAndModel MarkAndModelData { get; set; }

        public List<SelectListItem> CarStatusSelectList { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var carModel = await
                (from model in _context.MarkAndModels
                 where (model.CarMark.Equals(MarkAndModelData.CarMark, StringComparison.OrdinalIgnoreCase))
                 && (model.CarModel.Equals(MarkAndModelData.CarModel, StringComparison.OrdinalIgnoreCase))
                 select model).FirstOrDefaultAsync();

            if(carModel == null)
            {
                carModel = MarkAndModelData;
                await _context.AddAsync(carModel);
                await _context.SaveChangesAsync();
            }

            CarData.MarkAndModelID = carModel.MarkAndModelId;
                
            _context.Cars.Add(CarData);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}