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
