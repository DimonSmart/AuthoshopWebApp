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

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public class IndexModel : PageModel, IWorkerPage
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public IndexModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
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

            OutputModel = await OutputWorkerModel
                .GetQuery(_context)
                .FirstOrDefaultAsync(item => item.WorkerID == id);

            if (OutputModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
