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
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class AddPoolExpertiseRefModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public AddPoolExpertiseRefModel(
            ApplicationDbContext context,
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

            var carData = await
                (from car in _context.Cars
                 where car.CarId == id
                 join model in _context.MarkAndModels
                 on car.MarkAndModelID equals model.MarkAndModelId
                 select new { car.CarId, model }).FirstOrDefaultAsync();

            if(carData==null)
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
                CarId = carData.CarId,
                WorkerId = workerId.Value,
                IssueDate = DateTime.Now,
            };

            MarkAndModel = carData.model;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, PoolExpertiseReference, Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            await _context.PoolExpertiseReferences.AddAsync(PoolExpertiseReference);
            await _context.SaveChangesAsync();

            return RedirectToPage("./ExpertiseReference", new { id = PoolExpertiseReference.CarId });
        }
    }
}