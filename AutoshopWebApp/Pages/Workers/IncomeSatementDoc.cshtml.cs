using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Authorization;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models.ForShow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Workers
{
    public class IncomeSatementDocModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IncomeSatementDocModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? month, int? year)
        {
            if(month==null || year==null)
            {
                return NotFound();
            }

            var isAuthorize =
                User.IsInRole(Constants.AdministratorRole) ||
                User.IsInRole(Constants.ManagerRole);

            if(!isAuthorize)
            {
                return new ChallengeResult();
            }

            IncomeStatement = await _context
                .GetIncomeStatements()
                .FirstOrDefaultAsync(x => x.Date.Month == month && x.Date.Year == year);

            return Page();
        }

        public IncomeStatement IncomeStatement { get; set; }
    }
}