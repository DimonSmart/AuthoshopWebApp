using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class ExpertiseReferenceModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExpertiseReferenceModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public class OutputModel
        {
            public PoolExpertiseReference PoolExpertise { get; set; }
            public Car Car { get; set; }
            public MarkAndModel MarkAndModel { get; set; }
            public Worker Worker { get; set; }
        }

        public OutputModel OutModel { get; set; }

        public bool ShowEditButton { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            OutModel = await
                (from exp in _context.PoolExpertiseReferences
                 where exp.CarId == id
                 join car in _context.Cars on exp.CarId equals car.CarId
                 join mark in _context.MarkAndModels on car.MarkAndModelID equals mark.MarkAndModelId
                 join worker in _context.Workers on exp.WorkerId equals worker.WorkerId
                 select new OutputModel
                 {
                     PoolExpertise = exp,
                     Car = car,
                     MarkAndModel = mark,
                     Worker = worker
                 }).AsNoTracking().FirstOrDefaultAsync();

            if(OutModel==null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var isWorkerExist = await _context.WorkerUsers.AnyAsync(x => x.UserID == user.Id);

            ShowEditButton = isWorkerExist && OutModel.Car.SaleStatus == SaleStatus.Expertise;

            return Page();
        }
    }
}