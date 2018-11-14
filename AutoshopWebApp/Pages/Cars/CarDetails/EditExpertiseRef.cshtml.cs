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

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class EditExpertiseRefModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditExpertiseRefModel(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
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
                 join car in _context.Cars on reference.CarId equals car.CarId
                 where reference.CarId == id
                 join mark in _context.MarkAndModels on car.MarkAndModelID equals mark.MarkAndModelId
                 select new { reference, mark }).FirstOrDefaultAsync();

            if (loadedExpData == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var workerId = await
                (from workerUser in _context.WorkerUsers
                 where workerUser.UserID == user.Id
                 select new int?(workerUser.WorkerID)).FirstOrDefaultAsync();

            if(workerId==null)
            {
                return NotFound();
            }

            PoolExpertiseReference = loadedExpData.reference;
            MarkAndModel = loadedExpData.mark;
            PoolExpertiseReference.WorkerId = workerId.Value;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
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
