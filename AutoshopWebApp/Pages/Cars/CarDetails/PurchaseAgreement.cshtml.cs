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

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class PurchaseAgreementModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public PurchaseAgreementModel(ApplicationDbContext context,
             IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var carQuery =
                from car in _context.Cars
                where car.BuyingPrice != null
                join markAndModel in _context.MarkAndModels
                on car.MarkAndModelID equals markAndModel.MarkAndModelId
                join seller in _context.ClientSellers
                on car.CarId equals seller.CarId
                join sellerStreet in _context.Streets
                on seller.StreetId equals sellerStreet.StreetId
                join stateRef in _context.CarStateRefId
                on car.CarId equals stateRef.CarId
                join worker in _context.Workers
                on seller.WorkerId equals worker.WorkerId
                join position in _context.Positions
                on worker.PositionId equals position.PositionId
                let finalPrice = car.BuyingPrice.Value - stateRef.ExpertisePrice
                select new
                {
                    car, markAndModel, seller, sellerStreet, finalPrice, position,
                    worker = new Worker
                    {
                        Firstname = worker.Firstname,
                        Lastname = worker.Lastname,
                        Patronymic = worker.Patronymic,
                    }
                };

            var queryData = await carQuery
                .FirstOrDefaultAsync(x => x.car.CarId == id);

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

            Worker = queryData.worker;
            Car = queryData.car;
            ClientSeller = queryData.seller;
            SellerStreet = queryData.sellerStreet;
            MarkAndModel = queryData.markAndModel;
            FinalPrice = queryData.finalPrice;
            WorkerPosition = queryData.position;

            isAuthorized = await _authorizationService
                .AuthorizeAsync(User, queryData.seller, Operations.Update);

            ShowEditButton = isAuthorized.Succeeded;

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