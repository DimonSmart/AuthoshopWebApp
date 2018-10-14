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

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public class AddTransactionModel : PageModel, IWorkerPage
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public AddTransactionModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            WorkerData = await WorkerCrossPage.FindWorkerById(_context, id.Value);

            return Page();
        }

        [BindProperty]
        public TransactionOrder TransactionOrder { get; set; }

        [BindProperty]
        public WorkerCrossPage WorkerData { get; set; }

        public IWorkerCrossPageData WorkerCrossPageData => WorkerData;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TransactionOrders.Add(TransactionOrder);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}