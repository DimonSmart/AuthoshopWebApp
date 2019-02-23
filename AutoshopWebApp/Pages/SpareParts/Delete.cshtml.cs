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
using AutoshopWebApp.Services;

namespace AutoshopWebApp.Pages.SpareParts
{
    public class DeleteModel : PageModel
    {
        private readonly ISparePartService _sparePartService;
        private readonly IAuthorizationService _authorizationService;

        public DeleteModel(ISparePartService sparePartService,
            IAuthorizationService authorizationService)
        {
            _sparePartService = sparePartService;
            _authorizationService = authorizationService;
        }

        public SparePart SparePart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sparePart = await _sparePartService.ReadAsync(id.Value);

            if (sparePart == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, sparePart, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            SparePart = sparePart;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService
               .AuthorizeAsync(User, new SparePart() { SparePartId = id.Value }, Operations.Delete);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            await _sparePartService.DeleteAsync(id.Value);

            return RedirectToPage("./Index");
        }
    }
}
