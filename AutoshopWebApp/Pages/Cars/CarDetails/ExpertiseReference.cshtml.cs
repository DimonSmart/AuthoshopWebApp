using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Authorization;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IAuthorizationService _authorizationService;

        public ExpertiseReferenceModel(ApplicationDbContext context,
            UserManager<IdentityUser> userManager, IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
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
                 join worker in _context.Workers on exp.WorkerId equals worker.WorkerId
                 select new OutputModel
                 {
                     PoolExpertise = exp,
                     Car = car,
                     MarkAndModel = car.MarkAndModel,
                     Worker = worker
                 }).AsNoTracking().FirstOrDefaultAsync();

            if(OutModel==null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, OutModel.PoolExpertise, Operations.Details);

            if(!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var isWorkerExist = await _context.WorkerUsers.AnyAsync(x => x.UserID == user.Id);

            isAuthorized = await _authorizationService
                 .AuthorizeAsync(User, OutModel.PoolExpertise, Operations.Update);

            ShowEditButton = isWorkerExist && isAuthorized.Succeeded;

            return Page();
        }
    }
}