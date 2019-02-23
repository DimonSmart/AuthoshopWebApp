using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Services
{
    public class SparePartService: ISparePartService, IRepositoryService<SparePart>
    {
        private readonly ApplicationDbContext _context;

        public SparePartService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(SparePart sparePart)
        {
            sparePart.MarkAndModel = await _context
                .AddMarkAndModelAsync(sparePart.MarkAndModel.CarMark, sparePart.MarkAndModel.CarModel);

            await _context.SpareParts.AddAsync(sparePart);
            await _context.SaveChangesAsync();
        }

        public async Task<SparePart> DeleteAsync(int id)
        {
            var sparePart = await _context.SpareParts.FindAsync(id);

            if (sparePart != null)
            {
                _context.SpareParts.Remove(sparePart);
                await _context.SaveChangesAsync();
            }

            return sparePart;
        }

        public Task<bool> IsExistAsync(int id)
        {
            return _context.SpareParts
                .AnyAsync(x => x.SparePartId == id);
        }

        public Task<List<SparePart>> ReadAllAsync()
        {
            return _context.SpareParts
                .Include(x => x.MarkAndModel)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<List<SparePart>> ReadAllAsync(string search)
        {
            var query = _context.SpareParts.Select(x => x);
            var substrings = search.Split(' ');

            foreach (var str in substrings)
            {
                query =
                    from partData in query
                    where partData.MarkAndModel.CarMark.Contains(str, StringComparison.OrdinalIgnoreCase) ||
                    partData.MarkAndModel.CarModel.Contains(str, StringComparison.OrdinalIgnoreCase) ||
                    partData.PartName.Contains(str, StringComparison.OrdinalIgnoreCase)
                    select partData;
            }

            return query
                .AsNoTracking()
                .Include(x => x.MarkAndModel)
                .ToListAsync();
        }

        public Task<SparePart> ReadAsync(int id)
        {
            return _context.SpareParts
                .Include(x => x.MarkAndModel)
                .FirstOrDefaultAsync(x => x.SparePartId == id);
        }

        public async Task UpdateAsync(SparePart sparePart)
        {
            sparePart.MarkAndModel = await _context
                .AddMarkAndModelAsync(sparePart.MarkAndModel.CarMark, sparePart.MarkAndModel.CarModel);

            _context.Attach(sparePart).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }
    }
}
