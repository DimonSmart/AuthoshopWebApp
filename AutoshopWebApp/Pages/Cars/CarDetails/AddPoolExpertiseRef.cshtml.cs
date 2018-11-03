using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class AddPoolExpertiseRefModel : PageModel
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AddPoolExpertiseRefModel(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var expertiseCheck = await _context.PoolExpertiseReferences
                .AnyAsync(x => x.CarId == id);

            if (expertiseCheck)
            {
                return RedirectToPage("ExpertiseReference", new { id = id.Value });
            }

            var carId = await
                (from car in _context.Cars
                 where car.CarId == id && car.SaleStatus == SaleStatus.Expertise
                 select new int?(car.CarId)).FirstOrDefaultAsync();

            if(carId==null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if(user==null)
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

            PoolExpertiseReference = new PoolExpertiseReference
            {
                CarId = carId.Value,
                WorkerId = workerId.Value,
                IssueDate = DateTime.Now,
            };

            return Page();
        }

        [BindProperty]
        public PoolExpertiseReference PoolExpertiseReference { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            await _context.PoolExpertiseReferences.AddAsync(PoolExpertiseReference);
            await _context.SaveChangesAsync();

            return RedirectToPage("./ExpertiseReference", new { id = PoolExpertiseReference.CarId });
        }
    }
}