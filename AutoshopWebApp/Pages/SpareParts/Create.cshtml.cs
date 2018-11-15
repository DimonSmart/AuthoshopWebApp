using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.SpareParts
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public CreateModel(ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SparePart SparePart { get; set; }

        [BindProperty]
        public MarkAndModel MarkAndModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorzed = await _authorizationService
                .AuthorizeAsync(User, SparePart, Operations.Create);

            if(!isAuthorzed.Succeeded)
            {
                return new ChallengeResult();
            }

            MarkAndModel = await _context
                .AddMarkAndModelAsync(MarkAndModel.CarMark, MarkAndModel.CarModel);

            SparePart.MarkAndModelId = MarkAndModel.MarkAndModelId;

            await _context.SpareParts.AddAsync(SparePart);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}