using System;
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
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class AddClientBuyerModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public AddClientBuyerModel(ApplicationDbContext context, UserManager<IdentityUser> userManager,
             IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var isClientExist = await _context.ClientBuyers
                .AnyAsync(x => x.CarId == id);

            if (isClientExist)
            {
                return RedirectToPage("./BuyPurchaseAgreement", new { id });
            }

            var query = await
                (from car in _context.Cars
                 where car.CarId == id
                 join mark in _context.MarkAndModels
                 on car.MarkAndModelID equals mark.MarkAndModelId
                 select new { mark, car.CarId }).FirstOrDefaultAsync();

            if(query==null)
            {
                return NotFound();
            }

            PaymentTypes = await PaymentType.GetSelectList(_context);

            MarkAndModel = query.mark;
            CarId = query.CarId;

            return Page();
        }

        [BindProperty]
        public ClientBuyer ClientBuyer { get; set; }

        [BindProperty]
        public Street Street { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public List<SelectListItem> PaymentTypes { get; set; }

        public int CarId { get; set; }

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

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var workerUser = await _context.WorkerUsers
                .FirstOrDefaultAsync(m => m.UserID == user.Id);

            if(workerUser==null)
            {
                return NotFound();
            }

            Street = await _context.AddStreetAsync(Street.StreetName);
            ClientBuyer.StreetId = Street.StreetId;
            ClientBuyer.WorkerId = workerUser.WorkerID;

            _context.ClientBuyers.Add(ClientBuyer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = ClientBuyer.CarId });
        }
    }
}