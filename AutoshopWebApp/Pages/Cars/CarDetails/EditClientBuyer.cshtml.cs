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
    public class EditClientBuyerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _manager;
        private readonly IAuthorizationService _authorizationService;

        public EditClientBuyerModel(ApplicationDbContext context, UserManager<IdentityUser> manager,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _manager = manager;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ClientBuyer ClientBuyer { get; set; }

        [BindProperty]
        public Street Street { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public List<SelectListItem> PaymentTypes { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var queryData = await
                (from car in _context.Cars
                 where car.CarId == id
                 join markAndModel in _context.MarkAndModels
                 on car.MarkAndModelID equals markAndModel.MarkAndModelId
                 join buyer in _context.ClientBuyers
                 on car.CarId equals buyer.CarId
                 join street in _context.Streets
                 on buyer.StreetId equals street.StreetId
                 select new { markAndModel, buyer, street })
                 .FirstOrDefaultAsync();

            if(queryData==null)
            {
                return NotFound();
            }

            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, queryData.buyer, Operations.Update);

            if(!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            PaymentTypes = await PaymentType.GetSelectList(_context);

            ClientBuyer = queryData.buyer;
            Street = queryData.street;
            MarkAndModel = queryData.markAndModel;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, ClientBuyer, Operations.Update);

            if (!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            var user = await _manager.GetUserAsync(User);

            if(user==null)
            {
                return NotFound();
            }

            var workerId = await _context
                .FindWorkerIdByUserIdAsync(user.Id);

            if(workerId == null)
            {
                return NotFound();
            }

            Street = await _context.AddStreetAsync(Street.StreetName);
            ClientBuyer.WorkerId = workerId.Value;
            ClientBuyer.StreetId = Street.StreetId;

            _context.Attach(ClientBuyer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientBuyerExists(ClientBuyer.ClientBuyerId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./BuyPurchaseAgreement", new { id = ClientBuyer.CarId });
        }

        private bool ClientBuyerExists(int id)
        {
            return _context.ClientBuyers.Any(e => e.ClientBuyerId == id);
        }
    }
}
