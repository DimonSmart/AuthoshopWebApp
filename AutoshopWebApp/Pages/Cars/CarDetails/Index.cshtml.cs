using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using AutoshopWebApp.Models.ForShow;
using Microsoft.AspNetCore.Identity;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public OutputCarModel CarData { get; set; }

        public bool ShowPoolExpertiseButton { get; set; }

        public bool ShowAddExpertiseButton { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CarData = await
                (from car in _context.Cars
                 where id == car.CarId
                 join mark in _context.MarkAndModels on car.MarkAndModelID equals mark.MarkAndModelId
                 select new OutputCarModel
                 {
                     Car = car,
                     MarkAndModel = mark,
                 }).AsNoTracking().FirstOrDefaultAsync();

            var user = await _userManager.GetUserAsync(User);

            if (CarData == null || user == null)
            {
                return NotFound();
            }

            var isWorkerExist = await _context.WorkerUsers.AnyAsync(x => user.Id == x.UserID);
            var isPoolExpertiseExist = await _context.PoolExpertiseReferences.AnyAsync(x => x.CarId == id);

            ShowPoolExpertiseButton = (isWorkerExist || isPoolExpertiseExist) 
                && CarData.Car.SaleStatus == SaleStatus.Expertise;

            ShowAddExpertiseButton = CarData.Car.SaleStatus == SaleStatus.Expertise 
                && isPoolExpertiseExist;


            return Page();
        }
    }
}
