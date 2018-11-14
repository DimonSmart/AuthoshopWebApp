using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using AutoshopWebApp.Models.ForShow;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public class AddTransactionModel : PageModel, IWorkerPage
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public AddTransactionModel(ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var isAuthorize =
                User.IsInRole(Constants.AdministratorRole) ||
                User.IsInRole(Constants.ManagerRole);

            if (!isAuthorize)
            {
                return new ChallengeResult();
            }

            return await RedisplayPage(id.Value);
        }

        public WorkerCrossPage WorkerCrossPage { get; set; }

        [BindProperty]
        public TransactionOrder TransactionOrder { get; set; }

        public IList<SelectListItem> Positions { get; set; }

        public IWorkerCrossPageData WorkerCrossPageData => WorkerCrossPage;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await RedisplayPage(TransactionOrder.WorkerId);
            }


            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, TransactionOrder, Operations.Create);

            if(!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            var worker = _context.Workers
                .FirstOrDefault(item => TransactionOrder.WorkerId == item.WorkerId);

            if(worker==null)
            {
                return NotFound();
            }

            worker.PositionId = TransactionOrder.PositionId;

            _context.Attach(worker).Property(item => item.PositionId).IsModified = true;

            await _context.TransactionOrders.AddAsync(TransactionOrder);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = TransactionOrder.WorkerId});
        }

        private async Task<IActionResult> RedisplayPage(int id)
        {
            WorkerCrossPage = await WorkerCrossPage.FindWorkerDataById(_context, id);

            if(WorkerCrossPage == null)
            {
                return NotFound();
            }

            Positions = await Position.GetSelectListItems(_context);

            if(Positions == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}