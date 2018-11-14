using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class IncomeStatementModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IncomeStatementModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var isAuthorize =
                User.IsInRole(Constants.AdministratorRole) ||
                User.IsInRole(Constants.ManagerRole);

            if(!isAuthorize)
            {
                return new ChallengeResult();
            }

            DateTo = DateTime.Today;
            DateFrom = DateTo.AddYears(-1);

            IncomeStatement = await GetIncomeStatementsListAsync();

            return Page();
        }

        public IList<IncomeStatement> IncomeStatement { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var isAuthorize =
                User.IsInRole(Constants.AdministratorRole) ||
                User.IsInRole(Constants.ManagerRole);

            if (!isAuthorize)
            {
                return new ChallengeResult();
            }

            IncomeStatement = await GetIncomeStatementsListAsync();
            return Page();
        }

        

        [BindProperty]
        [Display(Name = "От:")]
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }

        [BindProperty]
        [Display(Name = "До:")]
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        private async Task<List<IncomeStatement>> GetIncomeStatementsListAsync()
        { 
            return await
                 (from data in _context.GetIncomeStatements()
                  where data.Date < DateTo && data.Date >= DateFrom
                  select data).AsNoTracking().ToListAsync();
        }
    }
}