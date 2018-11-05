﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class EditClientSellerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EditClientSellerModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public ClientSeller ClientSeller { get; set; }

        [BindProperty]
        public Street Street { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var workerUser = await _context.WorkerUsers
                .FirstOrDefaultAsync(m => m.UserID == user.Id);

            if (workerUser == null)
            {
                return NotFound();
            }

            var queryData = await
                (from seller in _context.ClientSellers
                 where seller.CarId == id
                 join street in _context.Streets
                 on seller.StreetId equals street.StreetId
                 select new { seller, street })
                 .AsNoTracking().FirstOrDefaultAsync();

            if(queryData == null)
            {
                return NotFound();
            }

            ClientSeller = queryData.seller;
            ClientSeller.WorkerId = workerUser.WorkerID;

            Street = queryData.street;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var street = await _context.AddStreetAsync(Street.StreetName);

            ClientSeller.StreetId = street.StreetId;

            _context.Attach(ClientSeller).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientSellerExists(ClientSeller.ClientSellerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./PurchaseAgreement", new { id = ClientSeller.CarId });
        }

        private bool ClientSellerExists(int id)
        {
            return _context.ClientSellers.Any(e => e.ClientSellerId == id);
        }
    }
}
