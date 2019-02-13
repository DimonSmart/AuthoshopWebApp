using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using System.ComponentModel.DataAnnotations;
using AutoshopWebApp.Models.ForShow;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public class IndexModel : PageModel, IWorkerPage
    {
        private readonly ApplicationDbContext _context;
        public readonly IAuthorizationService _authorizationService;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context,
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        
        public OutputWorkerModel OutputModel { get; set; }

        [BindProperty]
        public int WorkerId { get; set; }

        public IWorkerCrossPageData WorkerCrossPageData => OutputModel;

        public bool IsAuthDelete { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OutputModel = await
                (from worker in _context.Workers
                 .Include(x => x.Street)
                 .Include(x => x.Position)
                 where worker.WorkerId == id
                 select new OutputWorkerModel { Worker = worker })
                .FirstOrDefaultAsync();

            if(OutputModel == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, OutputModel.Worker, Operations.Details);

            if(!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            if (OutputModel == null)
            {
                return NotFound();
            }

            IsAuthDelete = (await _authorizationService
                .AuthorizeAsync(User, OutputModel.Worker, Operations.Delete))
                .Succeeded;


            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var data = await _context.Workers
                .FirstOrDefaultAsync(x => x.WorkerId == WorkerId);

            if(data == null)
            {
                return NotFound();
            }

            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, data, Operations.Delete);
            
            if(!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            _context.Workers.Remove(data);

            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
