using System;
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
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class EditClientSellerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public EditClientSellerModel(ApplicationDbContext context, UserManager<IdentityUser> userManager,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ClientSeller ClientSeller { get; set; }

        [BindProperty]
        public Street Street { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

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
                 select new
                 {
                     seller,
                     seller.Street,
                     seller.Car.MarkAndModel
                 }).AsNoTracking().FirstOrDefaultAsync();

            if(queryData == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, queryData.seller, Operations.Update);

            if(!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            ClientSeller = queryData.seller;
            ClientSeller.WorkerId = workerUser.WorkerID;

            MarkAndModel = queryData.MarkAndModel;
            Street = queryData.Street;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, ClientSeller, Operations.Update);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
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
