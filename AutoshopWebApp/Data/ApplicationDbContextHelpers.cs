﻿using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Data
{
    public partial class ApplicationDbContext
    {
        public async Task AddUserToWorkerAsync(int workerId, string userId)
        {
            var workerUser = new WorkerUser
            {
                WorkerID = workerId,
                UserID = userId
            };
            await WorkerUsers.AddAsync(workerUser);
            await SaveChangesAsync();
        }

        public async Task RemoveUserFromWorkerAsync(int workerId)
        {
            var workerUser = WorkerUsers.FirstOrDefault(item => item.WorkerID == workerId);
            if (workerUser == null) return;

            WorkerUsers.Remove(workerUser);
            await SaveChangesAsync();
        }

        public async Task<IdentityUser> FindUserByWorkerIdAsync(int workerId)
        {
            return await
                (from workerUser in WorkerUsers
                 where workerUser.WorkerID == workerId
                 join user in Users on workerUser.UserID equals user.Id
                 select user).FirstOrDefaultAsync();
        }

        public async Task<Street> AddStreetAsync(string street)
        {
            var findedStreet = await Streets
                .FirstOrDefaultAsync(m => m.StreetName.Equals(street, StringComparison.OrdinalIgnoreCase));

            if (findedStreet==null)
            {
                findedStreet = new Street
                {
                    StreetName = street
                };
                await AddAsync(findedStreet);
                await SaveChangesAsync();
            }

            return findedStreet;
        }


        void InitQueries()
        {
            BuyingOrders =
                from seller in ClientSellers
                join car in Cars on seller.CarId equals car.CarId
                join street in Streets on seller.StreetId equals street.StreetId
                join mark in MarkAndModels on car.MarkAndModelID equals mark.MarkAndModelId
                join carReference in CarStateRefId on car.CarId equals carReference.CarStateRefId
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
                    IssuedBy = seller.OwnDocIssuedBy,
                    BodyNumber = car.BodyNumber,
                    EngineNumber = car.EngineNumber,
                    ChassisNumber = car.ChassisNumber,
                    Run = car.Run
                };
        }
    }
}
