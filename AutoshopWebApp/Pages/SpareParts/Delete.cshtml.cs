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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public DeleteModel(ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public SparePart SparePart { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query =
                from part in _context.SpareParts
                join mark in _context.MarkAndModels
                on part.MarkAndModelId equals mark.MarkAndModelId
                select new { part, mark };

            var queryData = await query.FirstOrDefaultAsync(m => m.part.SparePartId == id);

            if (queryData == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, queryData.part, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            SparePart = queryData.part;
            MarkAndModel = queryData.mark;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SparePart = await _context.SpareParts.FindAsync(id);

            var isAuthorized = await _authorizationService
               .AuthorizeAsync(User, SparePart, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            if (SparePart != null)
            {
                _context.SpareParts.Remove(SparePart);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
