using AutoshopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Services
{
    public interface ISparePartService
    {
        Task<List<SparePart>> ReadAllAsync();
        Task<List<SparePart>> ReadAllAsync(string search);
        Task CreateAsync(SparePart sparePart);
        Task<SparePart> ReadAsync(int id);
        Task UpdateAsync(SparePart sparePart);
        Task<SparePart> DeleteAsync(int id);
        Task<bool> IsExistAsync(int id);
    }
}
