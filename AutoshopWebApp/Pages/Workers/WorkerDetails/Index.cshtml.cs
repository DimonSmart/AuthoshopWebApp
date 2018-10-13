using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public class IndexModel : PageModel, IWorkerPage
    {
        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;

        public IndexModel(AutoshopWebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public class OutputModel: IWorkerCrossPageData
        {
            public int WorkerID { get; set; }
            [Display(Name = "Имя")]
            public string Firstname { get; set; }
            [Display(Name = "Фамилия")]
            public string Lastname { get; set; }
            [Display(Name = "Отчество")]
            public string Patronymic { get; set; }
            [Display(Name = "Дата рождения")]
            public DateTime BornDate { get; set; }
            [Display(Name = "Улица")]
            public string Street { get; set; }
            [Display(Name = "№ дома")]
            public int HouseNumber { get; set; }
            [Display(Name = "№ квартиры")]
            public int ApartmentNumber { get; set; }
            [Display(Name = "Должность")]
            public string Position { get; set; }
        }

        public OutputModel Worker { get; set; }
        public IWorkerCrossPageData WorkerCrossPage => Worker;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Worker = await
                (from worker in _context.Workers
                 join street in _context.Streets on worker.StreetId equals street.StreetId
                 join position in _context.Positions on worker.PositionId equals position.PositionId
                 select new OutputModel
                 {
                     WorkerID = worker.WorkerId,
                     Firstname = worker.Firstname,
                     Lastname = worker.Lastname,
                     Patronymic = worker.Patronymic,
                     BornDate = worker.BornDate,
                     Street = street.StreetName,
                     HouseNumber = worker.HouseNumber,
                     ApartmentNumber = worker.ApartmentNumber,
                     Position = position.PositionName,
                 }).FirstOrDefaultAsync();

            if (Worker == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
