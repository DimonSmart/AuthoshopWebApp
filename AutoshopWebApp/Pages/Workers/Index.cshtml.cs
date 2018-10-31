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
            public int WorkerId { get; set; }

            [Display(Name = "Имя")]
            public string Firstname { get; set; }

            [Display(Name = "Фамилия")]
            public string Lastname { get; set; }

            [Display(Name = "Отчество")]
            public string Patronymic { get; set; }

            [Display(Name = "Должность")]
            public string Position { get; set; }
        }

        public IList<WorkerModel> Worker { get;set; }

        public async Task OnGetAsync(string search)
        {
            var workerQuery =
                from worker in _context.Workers
                join position in _context.Positions on worker.PositionId equals position.PositionId
                select new WorkerModel
                {
                    WorkerId = worker.WorkerId,
                    Firstname = worker.Firstname,
                    Lastname = worker.Lastname,
                    Patronymic = worker.Patronymic,
                    Position = position.PositionName,
                };

            if(!string.IsNullOrEmpty(search))
            {
                var splitted = search.Split(' ');
                foreach (var item in splitted)
                {
                    workerQuery =
                        from worker in workerQuery
                        where worker.Firstname.Contains(item) ||
                        worker.Lastname.Contains(item) ||
                        worker.Patronymic.Contains(item) ||
                        worker.Position.Contains(item)
                        select worker;
                }
            }

            Worker = await workerQuery
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
