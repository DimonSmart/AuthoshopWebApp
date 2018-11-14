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
using AutoshopWebApp.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace AutoshopWebApp.Pages.Workers
{
    public class AddWorkerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public readonly IAuthorizationService _authorizationService;

        public AddWorkerModel(ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public Worker Worker { get; set; }

        [BindProperty]
        public Street Street { get; set; }

        public IList<SelectListItem> Positions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var isAuthorize =
                User.IsInRole(Constants.AdministratorRole) ||
                User.IsInRole(Constants.ManagerRole);

            if(!isAuthorize)
            {
                return new ChallengeResult();
            }

            Positions = await Position.GetSelectListItems(_context);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, Worker, Operations.Create);

            if(!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            var street = await _context.Streets
                .FirstOrDefaultAsync(item => 
                item.StreetName.Equals(Street.StreetName, StringComparison.OrdinalIgnoreCase));

            if(street==null)
            {
                street = Street;
                _context.Streets.Add(street);
                await _context.SaveChangesAsync();
            }

            Worker.StreetId = street.StreetId;     

            _context.Workers.Add(Worker);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}