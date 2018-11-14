using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using AutoshopWebApp.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace AutoshopWebApp.Pages.Workers
{
    public class PositionsAddModel : PageModel
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;
        public readonly IAuthorizationService _authorizationService;

        public PositionsAddModel(AutoshopWebApp.Data.ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public IActionResult OnGet()
        {
            var isAuthorize =
                User.IsInRole(Constants.AdministratorRole) ||
                User.IsInRole(Constants.ManagerRole);

            if(!isAuthorize)
            {
                return new ChallengeResult();
            }

            return Page();
        }

        [BindProperty]
        public Position Position { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, Position, Operations.Create);

            if(!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            _context.Positions.Add(Position);
            await _context.SaveChangesAsync();

            return RedirectToPage("./PositionsMain");
        }
    }
}