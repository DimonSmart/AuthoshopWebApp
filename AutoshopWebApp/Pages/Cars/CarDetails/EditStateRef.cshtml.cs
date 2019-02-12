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

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class EditStateRefModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public EditStateRefModel(ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public CarStateRef CarStateRef { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queryData = await
                (from stateRef in _context.CarStateRefId
                 where stateRef.CarId == id
                 select new { stateRef, stateRef.Car.MarkAndModel }
                ).FirstOrDefaultAsync();
                


            if (queryData == null)
            {
                return RedirectToPage("./AddExpertiseRef", new { id = id.Value });
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, queryData.stateRef, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            CarStateRef = queryData.stateRef;
            MarkAndModel = queryData.MarkAndModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = await _authorizationService
               .AuthorizeAsync(User, CarStateRef, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            _context.Attach(CarStateRef).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarStateRefExists(CarStateRef.CarStateRefId))
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

        private bool CarStateRefExists(int id)
        {
            return _context.CarStateRefId.Any(e => e.CarStateRefId == id);
        }
    }
}
