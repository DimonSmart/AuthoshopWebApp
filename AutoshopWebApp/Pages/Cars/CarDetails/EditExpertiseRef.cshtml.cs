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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class EditExpertiseRefModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public EditExpertiseRefModel(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public PoolExpertiseReference PoolExpertiseReference { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loadedExpData = await
                (from reference in _context.PoolExpertiseReferences
                 select new { reference, reference.Car.MarkAndModel }
                 ).FirstOrDefaultAsync();

            if (loadedExpData == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, loadedExpData.reference, Operations.Update);

            if(!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var workerId = await
                (from workerUser in _context.WorkerUsers
                 where workerUser.IdentityUserId == user.Id
                 select new int?(workerUser.WorkerId)).FirstOrDefaultAsync();

            if(workerId==null)
            {
                return NotFound();
            }

            PoolExpertiseReference = loadedExpData.reference;
            MarkAndModel = loadedExpData.MarkAndModel;
            PoolExpertiseReference.WorkerId = workerId.Value;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, PoolExpertiseReference, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            _context.Attach(PoolExpertiseReference).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoolExpertiseReferenceExists(PoolExpertiseReference.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./ExpertiseReference", new { id = PoolExpertiseReference.CarId});
        }

        private bool PoolExpertiseReferenceExists(int id)
        {
            return _context.PoolExpertiseReferences.Any(e => e.Id == id);
        }
    }
}
