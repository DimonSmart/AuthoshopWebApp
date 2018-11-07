using AutoshopWebApp.Models;
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
            var findedMark = await MarkAndModels
                .FirstOrDefaultAsync(x => x.CarMark.Equals(mark, StringComparison.OrdinalIgnoreCase)
                || x.CarModel.Equals(model, StringComparison.OrdinalIgnoreCase));

            if(findedMark == null)
            {
                findedMark = new MarkAndModel
                {
                    CarMark = mark,
                    CarModel = model
                };
                await AddAsync(findedMark);
                await SaveChangesAsync();
            }

            return findedMark;
        }
        
    }
}
