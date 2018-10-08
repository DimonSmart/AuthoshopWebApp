using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;

namespace AutoshopWebApp.Pages.Workers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public class WorkerModel
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Patronymic { get; set; }
            public string Position { get; set; }
        }

        public IList<WorkerModel> Worker { get;set; }

        public async Task OnGetAsync()
        {
            var workerQuery =
                from worker in _context.Workers
                join position in _context.Positions on worker.PositionId equals position.PositionId
                select new WorkerModel
                {
                    Firstname = worker.Firstname,
                    Lastname = worker.Lastname,
                    Patronymic = worker.Patronymic,
                    Position = position.PositionName,
                };

            Worker = await workerQuery.ToListAsync();
        }
    }
}
