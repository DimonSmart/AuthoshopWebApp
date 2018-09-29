using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Data
{
    public partial class ApplicationDbContext
    {
        void InitQueries()
        {
            BuyingOrders =
                from seller in ClientSellers
                join car in Cars on seller.CarId equals car.CarId
                join street in Streets on seller.StreetId equals street.StreetId
                join mark in MarkAndModels on car.MarkAndModelID equals mark.MarkAndModelId
                join carReference in CarReferences on car.CarId equals carReference.CarReferenceId
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
    }
}
