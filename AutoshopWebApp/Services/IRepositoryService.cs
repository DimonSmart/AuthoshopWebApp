using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Services
{
    interface IRepositoryService<TEntity>
    {
        Task<List<TEntity>> ReadAllAsync();
        Task<List<TEntity>> ReadAllAsync(string search);
        Task CreateAsync(TEntity car);
        Task<TEntity> ReadAsync(int id);
        Task UpdateAsync(TEntity car);
        Task<TEntity> DeleteAsync(int carId);
        Task<bool> IsExistAsync(int id);
    }
}
