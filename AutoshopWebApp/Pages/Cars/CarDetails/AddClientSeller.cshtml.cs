﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class AddClientSellerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AddClientSellerModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var isClientExist = _context.ClientSellers.Any(x => x.CarId == id);

            if(isClientExist)
            {
                return RedirectToPage("./PurchaseAgreement", new { id });
            }

            var user = await _userManager.GetUserAsync(User);

            if(user == null)
            {
                return NotFound();
            }

            var workerUser = await _context.WorkerUsers
                .FirstOrDefaultAsync(m => m.UserID == user.Id);

            if(workerUser == null)
            {
                return NotFound();
            }

            WorkerId = workerUser.WorkerID;

            var carQueryData = await
                (from car in _context.Cars
                where car.CarId == id
                join markAndModel in _context.MarkAndModels
                on car.MarkAndModelID equals markAndModel.MarkAndModelId
                select new
                {
                    car.CarId,
                    markAndModel
                }).FirstOrDefaultAsync();

            if(carQueryData == null)
            {
                return NotFound();
            }

            CarId = carQueryData.CarId;
            MarkAndModel = carQueryData.markAndModel;

            return Page();
        }

        [BindProperty]
        public ClientSeller ClientSeller { get; set; }

        [BindProperty]
        public Street Street { get; set; }

        public int WorkerId { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public int CarId;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Street = await _context.AddStreetAsync(Street.StreetName);
            ClientSeller.StreetId = Street.StreetId;

            _context.ClientSellers.Add(ClientSeller);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = ClientSeller.CarId });
        }
    }
}