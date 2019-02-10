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

        public IList<Car> OutputCarModels { get;set; }

        public async Task<IActionResult> OnGetAsync(string search)
        {
            var query =
                from car in _context.Cars.Include(x => x.MarkAndModel)
                select car;

            OutputCarModels = await query.AsNoTracking().ToListAsync();

            return Page();
        }
    }
}
