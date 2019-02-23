using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Services
{
    public class CarService : ICarService, IRepositoryService<Car>
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Car car)
        {
            car.MarkAndModel = await _context
                .AddMarkAndModelAsync(car.MarkAndModel.CarMark, car.MarkAndModel.CarModel);

            await _context.Cars.AddAsync(car);

            await _context.SaveChangesAsync();
        }

        public async Task<Car> DeleteAsync(int carId)
        {
            var car = await _context.Cars.FindAsync(carId);

            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }

            return car;
        }

        public Task<List<Car>> ReadAllAsync()
        {
            return _context.Cars
                .Include(x => x.MarkAndModel)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<Car>> ReadAllAsync(string search)
        {
            var query = _context.Cars.Select(x => x);
            var substrings = search.Split(' ');

            foreach (var str in substrings)
            {
                query =
                    from carData in query
                    where carData.MarkAndModel.CarMark.Contains(str, StringComparison.OrdinalIgnoreCase) ||
                    carData.MarkAndModel.CarModel.Contains(str, StringComparison.OrdinalIgnoreCase) ||
                    carData.Color.Contains(str, StringComparison.OrdinalIgnoreCase)
                    select carData;
            }

            return query
                .Include(x => x.MarkAndModel)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<Car> ReadAsync(int id)
        {
            return _context.Cars
                .Include(x => x.MarkAndModel)
                .FirstOrDefaultAsync(x => x.CarId == id);
        }

        public async Task UpdateAsync(Car car)
        {
            car.MarkAndModel = await _context
                .AddMarkAndModelAsync(car.MarkAndModel.CarMark, car.MarkAndModel.CarModel);

            _context.Attach(car).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public Task<bool> IsExistAsync(int id)
        {
            return _context.Cars
                .AnyAsync(x => x.CarId == id);
        }
    }
}
