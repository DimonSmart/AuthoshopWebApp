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

        public PoolExpertiseReference PoolExpertise { get; set; }

        public bool ShowEditButton { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            PoolExpertise = await
                (from exp in _context.PoolExpertiseReferences
                .Include(x => x.Car).ThenInclude(x => x.MarkAndModel)
                .Include(x => x.Worker)
                 where exp.CarId == id
                 select exp).AsNoTracking().FirstOrDefaultAsync();


            if(PoolExpertise==null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, PoolExpertise, Operations.Details);

            if(!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var isWorkerExist = await _context.WorkerUsers
                .AnyAsync(x => x.IdentityUserId == user.Id);

            isAuthorized = await _authorizationService
                 .AuthorizeAsync(User, PoolExpertise, Operations.Update);

            ShowEditButton = isWorkerExist && isAuthorized.Succeeded;

            return Page();
        }
    }
}