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

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public class TransactionListModel : PageModel, IWorkerPage
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public TransactionListModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }


        public class TransactionData
        {
            public Position Position { get; set; }
            public TransactionOrder TransactionOrder { get; set; }
        }

        public IList<TransactionData> TransactionOrder { get;set; }

        public IWorkerCrossPageData WorkerCrossPageData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            WorkerCrossPageData = await WorkerCrossPage.FindWorkerByIdAsync(_context, id.Value);

            TransactionOrder = await
                (from order in _context.TransactionOrders
                 join position in _context.Positions on order.PositionId equals position.PositionId
                 where order.WorkerId == id
                 select new TransactionData
                 {
                     Position = new Position { PositionName = position.PositionName },
                     TransactionOrder = new TransactionOrder
                     {
                         OrderDate = order.OrderDate,
                         OrderNumber = order.OrderNumber,
                         Reason = order.Reason,
                     }
                 }).ToListAsync();

            return Page();
        }
    }
}
