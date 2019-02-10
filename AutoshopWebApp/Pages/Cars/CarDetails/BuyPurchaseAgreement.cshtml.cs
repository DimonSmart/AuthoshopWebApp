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
                join car in _context.Cars.Include(x => x.MarkAndModel)
                on buyer.CarId equals car.CarId
                join paymentType in _context.PaymentTypes
                on buyer.PaymentTypeId equals paymentType.PaymentTypeId
                join buyerStreet in _context.Streets
                on buyer.StreetId equals buyerStreet.StreetId
                join worker in _context.Workers
                on buyer.WorkerId equals worker.WorkerId
                join workerPos in _context.Positions
                on worker.PositionId equals workerPos.PositionId
                select new
                {
                    buyer, car, paymentType, buyerStreet, workerPos,
                    worker = new Worker
                    {
                        Firstname = worker.Firstname,
                        Lastname = worker.Lastname,
                        Patronymic = worker.Patronymic
                    }
                };

            var buyData = await buyQuery
                .FirstOrDefaultAsync(x => x.car.CarId == id);

            if(buyData == null)
            {
                return NotFound();
            }

            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, buyData.buyer, Operations.Details);

            if(!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            ClientBuyer = buyData.buyer;
            Car = buyData.car;
            MarkAndModel = buyData.car.MarkAndModel;
            PaymentType = buyData.paymentType;
            BuyerStreet = buyData.buyerStreet;
            Position = buyData.workerPos;
            Worker = buyData.worker;

            var isWorkerExist = await _context.WorkerUsers
                .AnyAsync(x => x.UserID == user.Id);

            isAuthorize = await _authorizationService
                .AuthorizeAsync(User, buyData.buyer, Operations.Update);

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