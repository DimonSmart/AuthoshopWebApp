using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;

namespace AutoshopWebApp.Pages.Workers
{
    public class PositionsMainModel : PageModel
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public PositionsMainModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Position> Position { get;set; }

        public async Task OnGetAsync()
        {
            Position = await _context
                .Positions.AsNoTracking()
                .ToListAsync();
        }
    }
}
