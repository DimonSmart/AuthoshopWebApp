using AutoshopWebApp.Models;
using AutoshopWebApp.Models.ForShow;
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

        public async Task<int?> FindWorkerIdByUserIdAsync(string userId)
        {
            return await
                (from workerUser in WorkerUsers
                 where workerUser.UserID == userId
                 select new int?(workerUser.WorkerID))
                .FirstOrDefaultAsync();
        }

        public IQueryable<IncomeStatement> GetIncomeStatements()
        {
            var querySelling =
               from seller in ClientSellers
               join sellingCar in Cars
               on seller.CarId equals sellingCar.CarId
               where sellingCar.BuyingPrice != null
               group sellingCar.BuyingPrice.Value
               by new { seller.SellingDate.Month, seller.SellingDate.Year }
               into sellingByMonth
               select new { sellingByMonth.Key, loss = sellingByMonth.Sum() };

            var queryBuying =
                from buyer in ClientBuyers
                join buyingCar in Cars
                on buyer.CarId equals buyingCar.CarId
                where buyingCar.SellingPrice != null
                group buyingCar.SellingPrice.Value
                by new { buyer.BuyDate.Month, buyer.BuyDate.Year }
                into buyingByMonth
                select new { buyingByMonth.Key, profit = buyingByMonth.Sum() };

            var queryLeftJoin =
                from sellData in querySelling
                join buyData in queryBuying
                on sellData.Key equals buyData.Key
                into buyQueryData
                from buyData in buyQueryData.DefaultIfEmpty()
                select new { sellData, buyData };

            var queryRightJoin =
                from buyData in queryBuying
                join sellData in querySelling
                on buyData.Key equals sellData.Key
                into sellQueryData
                from sellData in sellQueryData.DefaultIfEmpty()
                select new { sellData, buyData };

            var mainQuery =
                from data in queryLeftJoin.Union(queryRightJoin)
                let profit = data.buyData == null ? 0 : data.buyData.profit
                let loss = data.sellData == null ? 0 : data.sellData.loss
                let date = data.buyData == null ? data.sellData.Key : data.buyData.Key
                select new IncomeStatement
                {
                    Date = new DateTime(date.Year, date.Month, 1),
                    Profit = profit,
                    Loss = loss,
                    Sum = profit - loss,
                };

            return mainQuery;
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

        public async Task<MarkAndModel> AddMarkAndModelAsync(string mark, string model)
        {
            var findedMark = await
                (from markData in MarkAndModels
                 where markData.CarMark.Equals(mark, StringComparison.OrdinalIgnoreCase)
                 && markData.CarModel.Equals(model, StringComparison.OrdinalIgnoreCase)
                 select markData
                ).FirstOrDefaultAsync();

            if(findedMark == null)
            {
                findedMark = new MarkAndModel
                {
                    CarMark = mark,
                    CarModel = model
                };
                await MarkAndModels.AddAsync(findedMark);
                await SaveChangesAsync();
            }

            return findedMark;
        }
    }
}
