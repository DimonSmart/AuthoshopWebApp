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
    public class AddExpertiseRefModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AddExpertiseRefModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            CarStateRef = await _context.CarStateRefId.FirstOrDefaultAsync(x => x.CarId == id);



            return Page();
        }

        [BindProperty]
        public CarStateRef CarStateRef { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.CarStateRefId.Add(CarStateRef);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}