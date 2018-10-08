using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Workers
{
    public class AddWorkerModel : PageModel
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public AddWorkerModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Worker Worker { get; set; }

        [BindProperty]
        public Street Street { get; set; }

        [BindProperty]
        public int SelectedPosition { get; set; }

        public IList<SelectListItem> Positions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Positions = await
                (from pos in _context.Positions
                 select new SelectListItem
                 {
                     Value = pos.PositionId.ToString(),
                     Text = pos.PositionName
                 }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var street =
                (from str in _context.Streets
                 where str.StreetName == Street.StreetName
                 select str).FirstOrDefault();

            if(street==null)
            {
                _context.Streets.Add(street);
                await _context.SaveChangesAsync();
            }

            Worker.StreetId = street.StreetId;

            Worker.PositionId = SelectedPosition;
                

            _context.Workers.Add(Worker);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}