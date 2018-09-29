using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Utilities
{
    public static class DatabaseQueryHelpers
    {
        static IQueryable<BuyingOrder> GetOrderQuery(ApplicationDbContext context)
        {
            return
                from seller in context.ClientSellers
                join car in context.Cars on seller.CarId equals car.CarId
                join street in context.Streets on seller.StreetId equals street.StreetId
                join mark in context.MarkAndModels on car.MarkAndModelID equals mark.MarkAndModelId
                join carReference in context.CarReferences on car.CarId equals carReference.CarReferenceId
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

        static PoolExpertiseReference GetPoolExpertise(ApplicationDbContext context, int workerId, int carId)
        {
            var workerData =
                 (from worker in context.Workers
                 where worker.WorkerId == workerId
                 select new { worker.Firstname, worker.Lastname, worker.Patronymic })
                 .First();

            var carData =
                (from car in context.Cars
                 join carModel in context.MarkAndModels on car.MarkAndModelID equals carModel.MarkAndModelId
                 where car.CarId == carId
                 select new
                 {
                     carModel.CarMark,
                     carModel.CarModel,
                     car.Color,
                     car.RegNumber,
                     car.BodyNumber,
                     car.EngineNumber,
                     car.ChassisNumber
                 }).First();

            return new PoolExpertiseReference
            {
                WorkerFirstname = workerData.Firstname,
                WorkerLastname = workerData.Lastname,
                WorkerPatronymic = workerData.Patronymic,
                Mark = carData.CarMark,
                Model = carData.CarModel,
                Color = carData.Color,
                RegNumber = carData.RegNumber,
                BodyNumber = carData.BodyNumber,
                EngineNumber = carData.EngineNumber,
                ChassisNumber = carData.ChassisNumber
            };
        }
    }
}
