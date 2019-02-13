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
using Microsoft.AspNetCore.Authorization;

namespace AutoshopWebApp.Pages.Workers
{
    public class PositionDeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public readonly IAuthorizationService _authorizationService;

        public PositionDeleteModel(ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public Position Position { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Position = await _context.Positions.FirstOrDefaultAsync(m => m.PositionId == id);

            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, Position, Operations.Delete);

            if (!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            if (Position == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Position = await _context.Positions.FindAsync(id);

            if(Position == null)
            {
                return NotFound();
            }

            var isAuthorize = await _authorizationService
              .AuthorizeAsync(User, Position, Operations.Delete);

            if (!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            _context.Positions.Remove(Position);
            await _context.SaveChangesAsync();

            return RedirectToPage("./PositionsMain");
        }
    }
}
