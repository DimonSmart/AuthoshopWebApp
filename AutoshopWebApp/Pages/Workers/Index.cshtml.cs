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
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.Workers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public readonly IAuthorizationService AuthorizationService;

        public IndexModel(ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            AuthorizationService = authorizationService;
        }

        public IList<Worker> Worker { get;set; }

        public bool IsShowDetails { get; set; }

        public bool IsShowAddButton { get; set; }

        public async Task<IActionResult> OnGetAsync(string search)
        {
            var workerQuery =
                from worker in _context.Workers
                select new Worker
                {
                    WorkerId = worker.WorkerId,
                    Firstname = worker.Firstname,
                    Lastname = worker.Lastname,
                    Patronymic = worker.Patronymic,
                    Position = worker.Position
                };

            if (!string.IsNullOrEmpty(search))
            {
                var splitted = search.Split(' ');
                foreach (var item in splitted)
                {
                    workerQuery =
                        from worker in workerQuery
                        where worker.Firstname.Contains(item) ||
                        worker.Lastname.Contains(item) ||
                        worker.Patronymic.Contains(item) ||
                        worker.Position.PositionName.Contains(item)
                        select worker;
                }
            }

            Worker = await workerQuery
                .AsNoTracking()
                .ToListAsync();

            IsShowAddButton = IsShowDetails =
                User.IsInRole(Constants.AdministratorRole) ||
                User.IsInRole(Constants.ManagerRole);

            return Page();
        }
    }
}
