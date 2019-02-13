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
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public class TransactionListModel : PageModel, IWorkerPage
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public TransactionListModel(ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public OutputWorkerModel OutputData { get; set; }

        public IWorkerCrossPageData WorkerCrossPageData
            => OutputData;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isAuthorize = User.IsInRole(Constants.AdministratorRole) ||
                User.IsInRole(Constants.ManagerRole);

            if (!isAuthorize)
            {
                return new ChallengeResult();
            }

            var queryData =
                from worker in _context.Workers
                .Include(x => x.TransactionOrders).ThenInclude(x => x.Position)
                where worker.WorkerId == id
                select new OutputWorkerModel
                {
                    Worker = worker
                };

            OutputData = await queryData
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (OutputData == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
