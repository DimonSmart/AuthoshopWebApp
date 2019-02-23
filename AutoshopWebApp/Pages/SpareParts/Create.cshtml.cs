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
using AutoshopWebApp.Services;

namespace AutoshopWebApp.Pages.SpareParts
{
    public class CreateModel : PageModel
    {
        private readonly ISparePartService _sparePartService;
        private readonly IAuthorizationService _authorizationService;

        public CreateModel(ISparePartService sparePartService,
            IAuthorizationService authorizationService)
        {
            _sparePartService = sparePartService;
            _authorizationService = authorizationService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public SparePart SparePart { get; set; }

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

            await _sparePartService.CreateAsync(SparePart);

            return RedirectToPage("./Index");
        }
    }
}