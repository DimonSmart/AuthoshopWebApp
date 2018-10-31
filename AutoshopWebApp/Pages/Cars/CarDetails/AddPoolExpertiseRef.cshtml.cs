using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class AddPoolExpertiseRefModel : PageModel
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public AddPoolExpertiseRefModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PoolExpertiseReference PoolExpertiseReference { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PoolExpertiseReferences.Add(PoolExpertiseReference);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}