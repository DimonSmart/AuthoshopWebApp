using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class AddClientSellerModel : PageModel
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public AddClientSellerModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var queryData = await
                (from car in _context.Cars
                where car.CarId == id
                join markAndModel in _context.MarkAndModels
                on car.MarkAndModelID equals markAndModel.MarkAndModelId
                select new
                {
                    car.CarId,
                    markAndModel
                }).FirstOrDefaultAsync();

            if(queryData == null)
            {
                return NotFound();
            }

            CarId = queryData.CarId;
            MarkAndModel = queryData.markAndModel;

            return Page();
        }

        [BindProperty]
        public ClientSeller ClientSeller { get; set; }

        [BindProperty]
        public Street Street { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public int CarId;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Street = await _context.AddStreetAsync(Street.StreetName);
            ClientSeller.StreetId = Street.StreetId;

            _context.ClientSellers.Add(ClientSeller);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = ClientSeller.CarId });
        }
    }
}