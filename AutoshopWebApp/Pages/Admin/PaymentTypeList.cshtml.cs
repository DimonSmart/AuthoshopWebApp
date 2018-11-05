using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;

namespace AutoshopWebApp.Pages.Admin
{
    public class PaymentTypeListModel : PageModel
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public PaymentTypeListModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PaymentType PaymentType { get; set; }

        public IList<PaymentType> PaymentTypes { get;set; }

        public async Task OnGetAsync()
        {
            PaymentTypes = await _context.PaymentTypes
                .AsNoTracking().ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _context.PaymentTypes.AddAsync(PaymentType);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var paymentType = await _context.PaymentTypes
                .FirstOrDefaultAsync(x => x.PaymentTypeId == id);

            if(paymentType != null)
            {
                _context.PaymentTypes.Remove(paymentType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
