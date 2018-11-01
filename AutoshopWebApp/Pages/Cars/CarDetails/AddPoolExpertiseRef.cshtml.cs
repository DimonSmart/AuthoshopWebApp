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

            var carCheck = await _context.Cars
                .AnyAsync(x => (x.CarId == id) && (x.SaleStatus == SaleStatus.Expertise));

            if(!carCheck)
            {
                return NotFound();
            }

            var expertiseCheck = await _context.PoolExpertiseReferences
                .AnyAsync(x => x.Car.CarId == id);

            if(expertiseCheck)
            {
                return RedirectToPage("ExpertiseReference", new { id = id.Value });
            }

            CarId = id.Value;

            return Page();
        }

        [BindProperty]
        public PoolExpertiseReference PoolExpertiseReference { get; set; }

        [BindProperty]
        public int CarId { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            var workerData = await
                (from workerUser in _context.WorkerUsers
                 join worker in _context.Workers
                 on workerUser.WorkerID equals worker.WorkerId
                 where workerUser.UserID == user.Id
                 select worker).FirstOrDefaultAsync();

            if(workerData==null)
            {
                return NotFound();
            }

            var carData = await _context.Cars.FirstOrDefaultAsync(c => c.CarId == CarId);

            if(carData == null)
            {
                return NotFound();
            }

            PoolExpertiseReference.Worker = workerData;
            PoolExpertiseReference.Car = carData;


            await _context.PoolExpertiseReferences.AddAsync(PoolExpertiseReference);
            await _context.SaveChangesAsync();

            return RedirectToPage("./ExpertiseReference", new { id = carData.CarId });
        }
    }
}