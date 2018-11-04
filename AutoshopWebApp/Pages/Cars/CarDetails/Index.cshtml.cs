using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using AutoshopWebApp.Models.ForShow;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public class ExpertiseRefOutput
        {
            public PoolExpertiseReference PoolExpertiseReference { get; set; }
            [Display(Name = "Сотрудник")]
            public string WorkerName { get; set; }
        }

        public OutputCarModel CarData { get; set; }

        public CarStateRef CarStateRef { get; set; }

        public ExpertiseRefOutput ExpertiseRefData { get; set; }

        public bool ShowPoolExpertiseButton { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query =
                from car in _context.Cars
                where id == car.CarId
                join mark in _context.MarkAndModels
                on car.MarkAndModelID equals mark.MarkAndModelId

                join stateRef in _context.CarStateRefId
                on car.CarId equals stateRef.CarId into selectedStateRef
                from stateRef in selectedStateRef.DefaultIfEmpty()

                join expertiseRef in _context.PoolExpertiseReferences
                on car.CarId equals expertiseRef.CarId into seletedExpRef
                from expertiseRef in seletedExpRef.DefaultIfEmpty()

                join worker in _context.Workers
                on expertiseRef.WorkerId equals worker.WorkerId into selectedWorker
                from worker in selectedWorker.DefaultIfEmpty()

                select new
                {
                    carModel = new OutputCarModel
                    {
                        Car = car,
                        MarkAndModel = mark,
                    },
                    stateRef,
                    expertiseRefData = expertiseRef==null ? null : new ExpertiseRefOutput
                    {
                        PoolExpertiseReference = expertiseRef,
                        WorkerName = $"{worker.Lastname} {worker.Firstname[0]}.",
                    }
                };

            var queryData = await query.FirstOrDefaultAsync();

            if(queryData==null)
            {
                return NotFound();
            }

            CarStateRef = queryData.stateRef;
            CarData = queryData.carModel;
            ExpertiseRefData = queryData.expertiseRefData;

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var isWorkerExist = await _context.WorkerUsers.AnyAsync(x => user.Id == x.UserID);
            var isPoolExpertiseExist = await _context.PoolExpertiseReferences.AnyAsync(x => x.CarId == id);

            ShowPoolExpertiseButton = (isWorkerExist || isPoolExpertiseExist) 
                && CarData.Car.SaleStatus == SaleStatus.Expertise;

            return Page();
        }
    }
}
