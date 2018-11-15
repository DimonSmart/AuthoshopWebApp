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

            OutputModel = await OutputWorkerModel
                .GetQuery(_context)
                .AsNoTracking()
                .FirstOrDefaultAsync(item => item.WorkerID == id);

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
            var ordersQuery =
               from order in _context.TransactionOrders
               where order.WorkerId == WorkerId
               join position in _context.Positions
               on order.PositionId equals position.PositionId
               group new { order, position } 
               by order.WorkerId;

            var queryData =
                from worker in _context.Workers
                where worker.WorkerId == WorkerId
                join orders in ordersQuery
                on worker.WorkerId equals orders.Key
                into ordersData
                from orders in ordersData.DefaultIfEmpty()
                let ordersList = orders == null ? null : orders.ToList()
                join workerUser in _context.WorkerUsers
                on worker.WorkerId equals workerUser.WorkerID
                into workerUserQuery
                from workerUser in workerUserQuery.DefaultIfEmpty()
                select new { worker, ordersList, workerUser };

            var data = await queryData.FirstOrDefaultAsync();

            if(data == null)
            {
                return NotFound();
            }

            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, data.worker, Operations.Delete);
            
            if(!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            if (data.workerUser != null)
            {
                var user = await _userManager.FindByIdAsync(data.workerUser.UserID);
                await _userManager.DeleteAsync(user);
                _context.Remove(data.workerUser);
            }

            if(data.ordersList!=null)
            {
                foreach (var item in data.ordersList)
                {
                    _context.Remove(item);
                }
            }
           

            _context.Remove(data.worker);

            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
