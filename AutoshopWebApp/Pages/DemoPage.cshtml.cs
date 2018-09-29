using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;

namespace AutoshopWebApp.Pages
{
    public class DemoPageModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DemoPageModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var data =
                from seller in _context.ClientSellers
                join car in _context.Cars on seller.CarId equals car.CarId
                join street in _context.Streets on seller.StreetId equals street.StreetId
                join mark in _context.MarkAndModels on car.MarkAndModelID equals mark.MarkAndModelId
                join carReference in _context.CarReferences on car.CarId equals carReference.CarReferenceId
                select new BuyingOrder
                {
                    OrderNumber = seller.ClientSellerId,
                    SellingDate = seller.SellingDate,
                    LastName = seller.LastName,
                    Firstname = seller.Firstname,
                    Patronymic = seller.Patronymic,
                    PasNumber = seller.PasNumber,
                    StreetName = street.StreetName,
                    HouseNumber = seller.HouseNumber,
                    ApartmentNumber = seller.ApartmentNumber,
                    CarMark = mark.CarMark,
                    CarModel = mark.CarModel,
                    Color = car.Color,
                    ReleaseDate = car.ReleaseDate,
                    ReferenceNumber = carReference.ReferenceNumber,
                    ReferenceDate = carReference.ReferenceDate,
                    Expert = carReference.Expert,
                    ExpertisePrice = carReference.ExpertisePrice,
                    SellingPrice = car.SellingPrice ?? default(decimal),
                    DocName = seller.DocName,
                    DocNumber = seller.DocNumber,
                    IssueDate = seller.IssueDate,
                    IssuedBy = seller.IssuedBy,
                    BodyNumber = car.BodyNumber,
                    EngineNumber = car.EngineNumber,
                    ChassisNumber = car.ChassisNumber,
                    Run = car.Run
                };
        }



        /*public class IndexModel : PageModel
        {
            private readonly WebApplication1_Gluhovskiy.Models.WebApplication1_GluhovskiyContext _context;

            public IndexModel(WebApplication1_Gluhovskiy.Models.WebApplication1_GluhovskiyContext context)
            {
                _context = context;
            }

            public IList<FullWorkData> FullWorkDatas { get; set; }

            public async Task OnGetAsync()
            {
                FullWorkDatas = await _context.FullWorkDatas.ToListAsync();
            }
        }*/
    }
}