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
using AutoshopWebApp.Services;

namespace AutoshopWebApp.Pages.SpareParts
{
    public class EditModel : PageModel
    {
        private readonly ISparePartService _sparePartService;
        private readonly IAuthorizationService _authorizationService;

        public EditModel(ISparePartService sparePartService,
            IAuthorizationService authorizationService)
        {
            _sparePartService = sparePartService;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public SparePart SparePart { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queryData = await _sparePartService.ReadAsync(id.Value);

            if (queryData == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, queryData, Operations.Update);

            if(!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            SparePart = queryData;

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

            try
            {
                await _sparePartService.UpdateAsync(SparePart);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _sparePartService.IsExistAsync(SparePart.SparePartId))
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
    }
}
