using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Authorization;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class BuyPurchaseAgreementModel : PageModel
    {
        public readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public BuyPurchaseAgreementModel(ApplicationDbContext context,
             UserManager<IdentityUser> userManager, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var user = await _userManager
                .GetUserAsync(User);

            if(user==null)
            {
                return NotFound();
            }

            var buyQuery =
                from buyer in _context.ClientBuyers
                .Include(x => x.Car).ThenInclude(x => x.MarkAndModel)
                .Include(x => x.PaymentType)
                .Include(x => x.Street)
                .Include(x => x.Worker).ThenInclude(x => x.Position)
                select buyer;

            var buyData = await buyQuery
                .FirstOrDefaultAsync(x => x.CarId == id);

            if(buyData == null)
            {
                return NotFound();
            }

            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, buyData, Operations.Details);

            if(!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            ClientBuyer = buyData;
            Car = buyData.Car;
            MarkAndModel = buyData.Car.MarkAndModel;
            PaymentType = buyData.PaymentType;
            BuyerStreet = buyData.Street;
            Position = buyData.Worker.Position;
            Worker = buyData.Worker;

            var isWorkerExist = await _context.WorkerUsers
                .AnyAsync(x => x.UserID == user.Id);

            isAuthorize = await _authorizationService
                .AuthorizeAsync(User, buyData, Operations.Update);

            ShowEditButton = isWorkerExist && isAuthorize.Succeeded;

            return Page();
        }

        public ClientBuyer ClientBuyer { get; set; }

        public Car Car { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public PaymentType PaymentType { get; set; }
        
        public Street BuyerStreet { get; set; }

        public Position Position { get; set; }

        public Worker Worker { get; set; }

        public bool ShowEditButton { get; set; }
    }
}