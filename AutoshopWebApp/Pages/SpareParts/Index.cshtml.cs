using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.SpareParts
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<SparePart> SparePart { get;set; }

        public bool ShowDeleteButton { get; set; }

        public async Task<IActionResult> OnGetAsync(string search)
        {
            var query = _context.SpareParts.Select(sp => sp);

            if (!string.IsNullOrEmpty(search))
            {
                var substrings = search.Split(' ');

                foreach (var item in substrings)
                {
                    foreach (var str in substrings)
                    {
                        query =
                            from partData in query
                            where partData.MarkAndModel.CarMark.Contains(str, StringComparison.OrdinalIgnoreCase) ||
                            partData.MarkAndModel.CarModel.Contains(str, StringComparison.OrdinalIgnoreCase) ||
                            partData.PartName.Contains(str, StringComparison.OrdinalIgnoreCase)
                            select partData;
                    }
                }
            }

            SparePart = await query.AsNoTracking().ToListAsync();

            ShowDeleteButton = User.IsInRole(Constants.AdministratorRole);

            return Page();
        }
    }
}
