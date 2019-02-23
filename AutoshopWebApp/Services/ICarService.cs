using AutoshopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Services
{
    public interface ICarService
    {
        Task<List<Car>> ReadAllAsync();
        Task<List<Car>> ReadAllAsync(string search);
        Task CreateAsync(Car car);
        Task<Car> ReadAsync(int id);
        Task UpdateAsync(Car car);
        Task<Car> DeleteAsync(int carId);
        Task<bool> IsExistAsync(int id);
    }
}
