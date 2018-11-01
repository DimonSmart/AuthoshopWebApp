using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class ExpertiseReferenceModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExpertiseReferenceModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public PoolExpertiseReference PoolExpertise { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            PoolExpertise = await
                (from exp in _context.PoolExpertiseReferences
                 where exp.Car.CarId == id
                 select new PoolExpertiseReference
                 {
                     Car = exp.Car,
                     Worker = exp.Worker,
                     ClientFirstname = exp.ClientFirstname,
                     ClientLastname = exp.ClientLastname,
                     ClientPatronymic = exp.ClientPatronymic,
                     IssueDate = exp.IssueDate
                 }).AsNoTracking().FirstOrDefaultAsync();

            MarkAndModel = await _context.MarkAndModels
                .FirstOrDefaultAsync(x => x.MarkAndModelId == PoolExpertise.Car.MarkAndModelID);

            if(PoolExpertise==null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}