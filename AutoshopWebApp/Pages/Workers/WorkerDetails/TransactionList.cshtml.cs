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


        public class TransactionData
        {
            public Position Position { get; set; }
            public TransactionOrder TransactionOrder { get; set; }
        }

        public class Output:IWorkerCrossPageData
        {
            public string Firstname => Worker.Firstname;

            public string Lastname => Worker.Lastname;

            public string Patronymic => Worker.Patronymic;

            public int WorkerID => Worker.WorkerId;

            public Worker Worker { get; set; }

            public IList<TransactionData> TransactionOrders { get; set; }
        }

        public Output OutputData { get; set; }

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

            if(!isAuthorize)
            {
                return new ChallengeResult();
            }

            var ordersQuery =
                from order in _context.TransactionOrders
                join position in _context.Positions
                on order.PositionId equals position.PositionId
                group new TransactionData
                {
                    TransactionOrder = order,
                    Position = position
                } by order.WorkerId;

            var queryData =
                from worker in _context.Workers
                where worker.WorkerId == id
                join orders in ordersQuery
                on worker.WorkerId equals orders.Key
                into ordersQueryData
                from orders in ordersQueryData.DefaultIfEmpty()
                let ordersList = orders==null ? new List<TransactionData>(): orders.ToList()
                select new Output
                {
                    Worker = new Worker
                    {
                        WorkerId = worker.WorkerId,
                        Firstname = worker.Firstname,
                        Lastname = worker.Lastname,
                        Patronymic = worker.Patronymic
                    },
                    TransactionOrders = ordersList
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
