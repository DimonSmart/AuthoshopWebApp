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
using AutoshopWebApp.Models.ForShow;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public class EditModel : PageModel, IWorkerPage
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public EditModel(ApplicationDbContext context, 
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }
        
        [BindProperty]
        public OutputWorkerModel OutputModel { get; set; }

        public IWorkerCrossPageData WorkerCrossPageData => OutputModel;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return await RedisplayPage(id.Value);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await RedisplayPage(OutputModel.WorkerID);
            }

            var isAuthorize = await _authorizationService
               .AuthorizeAsync(User, OutputModel.Worker, Operations.Update);

            if (!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            var street = await _context.AddStreetAsync(OutputModel.Street.StreetName);

            OutputModel.Worker.StreetId = street.StreetId;

            _context.Attach(OutputModel.Worker).State = EntityState.Modified;
            _context.Entry(OutputModel.Worker).Property(x => x.PositionId).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(OutputModel.WorkerID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { id = OutputModel.WorkerID });
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.WorkerId == id);
        }

        private async Task<IActionResult> RedisplayPage(int id)
        {
            OutputModel = await OutputWorkerModel
                .GetQuery(_context)
                .FirstOrDefaultAsync(item => item.WorkerID == id);

            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, OutputModel.Worker, Operations.Update);

            if(!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            if (OutputModel == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
