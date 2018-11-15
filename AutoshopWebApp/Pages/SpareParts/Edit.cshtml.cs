using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.SpareParts
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public EditModel(AutoshopWebApp.Data.ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public SparePart SparePart { get; set; }

        [BindProperty]
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
                .AuthorizeAsync(User, queryData.part, Operations.Update);

            if(!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            SparePart = queryData.part;
            MarkAndModel = queryData.mark;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, SparePart, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            MarkAndModel = await _context.AddMarkAndModelAsync(MarkAndModel.CarMark, MarkAndModel.CarModel);

            SparePart.MarkAndModelId = MarkAndModel.MarkAndModelId;

            _context.Attach(SparePart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SparePartExists(SparePart.SparePartId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool SparePartExists(int id)
        {
            return _context.SpareParts.Any(e => e.SparePartId == id);
        }
    }
}
