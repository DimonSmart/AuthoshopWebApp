﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class BuyPurchaseAgreementModel : PageModel
    {
        public readonly ApplicationDbContext _context;

        public BuyPurchaseAgreementModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var buyQuery =
                from buyer in _context.ClientBuyers
                join car in _context.Cars
                on buyer.CarId equals car.CarId
                join markAndModel in _context.MarkAndModels
                on car.MarkAndModelID equals markAndModel.MarkAndModelId
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
                    buyer, car, markAndModel, paymentType, buyerStreet, workerPos,
                    worker = new Worker
                    {
                        Firstname = worker.Firstname,
                        Lastname = worker.Lastname,
                        Patronymic = worker.Patronymic
                    }
                };

            var buyData = await buyQuery
                .FirstOrDefaultAsync();

            if(buyData == null)
            {
                return NotFound();
            }

            ClientBuyer = buyData.buyer;
            Car = buyData.car;
            MarkAndModel = buyData.markAndModel;
            PaymentType = buyData.paymentType;
            BuyerStreet = buyData.buyerStreet;
            Position = buyData.workerPos;
            Worker = buyData.worker;

            return Page();
        }

        public ClientBuyer ClientBuyer { get; set; }

        public Car Car { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public PaymentType PaymentType { get; set; }
        
        public Street BuyerStreet { get; set; }

        public Position Position { get; set; }

        public Worker Worker { get; set; }
    }
}