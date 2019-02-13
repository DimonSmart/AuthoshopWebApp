using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoshopWebApp.Models;
using AutoshopWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;
using Microsoft.AspNetCore.Identity;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class PurchaseAgreementModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public PurchaseAgreementModel(ApplicationDbContext context,
             UserManager<IdentityUser> userManager, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);

            if(user==null)
            {
                return NotFound();
            }


            var carQuery =
                from seller in _context.ClientSellers
                .Include(x=> x.Car).ThenInclude(x=>x.MarkAndModel)
                .Include(x=> x.Street)
                .Include(x => x.Worker).ThenInclude(x => x.Position)
                join stateRef in _context.CarStateRefId
                on seller.CarId equals stateRef.CarId
                let finalPrice = seller.Car.BuyingPrice.Value - stateRef.ExpertisePrice
                select new { seller, stateRef, finalPrice };

            var queryData = await carQuery
                .FirstOrDefaultAsync(x => x.seller.CarId == id);

            if(queryData == null)
            {
                return NotFound();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, queryData.seller, Operations.Details);

            if (!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            Worker = queryData.seller.Worker;
            Car = queryData.seller.Car;
            ClientSeller = queryData.seller;
            SellerStreet = queryData.seller.Street;
            MarkAndModel = queryData.seller.Car.MarkAndModel;
            FinalPrice = queryData.finalPrice;
            WorkerPosition = queryData.seller.Worker.Position;

            isAuthorized = await _authorizationService
                .AuthorizeAsync(User, queryData.seller, Operations.Update);

            var isWorkerExist = await _context.WorkerUsers
                .AnyAsync(x => x.IdentityUserId == user.Id);

            ShowEditButton = isWorkerExist && isAuthorized.Succeeded;

            return Page();
        }

        public Worker Worker { get; set; }

        public Car Car { get; set; }

        public ClientSeller ClientSeller { get; set; }

        public Street SellerStreet { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public Position WorkerPosition { get; set; }

        public bool ShowEditButton { get; set; }

        [DataType(DataType.Currency)]
        public decimal FinalPrice { get; set; }
    }
}