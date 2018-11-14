using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.Workers
{
    public class PositionsMainModel : PageModel
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public PositionsMainModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Position> Position { get; set; }

        public bool IsShowDeleteButton { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var isAdmin = User.IsInRole(Constants.AdministratorRole);
            var isManager = User.IsInRole(Constants.ManagerRole);

            var isAuthorized = isAdmin || isManager;

            if(!isAuthorized)
            {
                return new ChallengeResult();
            }

            IsShowDeleteButton = isAdmin;


            Position = await _context
                .Positions.AsNoTracking()
                .ToListAsync();

            return Page();
        }
    }
}
