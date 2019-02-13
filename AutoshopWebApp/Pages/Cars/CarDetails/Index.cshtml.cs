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
using AutoshopWebApp.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager,
             IAuthorizationService authorizationService)
        {
            _context = context;
            _userManager = userManager;
            _authorizationService = authorizationService;
        }

        public class ExpertiseRefOutput
        {
            public PoolExpertiseReference PoolExpertiseReference { get; set; }
            [Display(Name = "Сотрудник")]
            public string WorkerName { get; set; }
        }

        public Car CarData { get; set; }

        public CarStateRef CarStateRef { get; set; }

        public ExpertiseRefOutput ExpertiseRefData { get; set; }

        public bool ShowCarEditButton { get; set; }

        public bool ShowExpertiseButton { get; set; }

        public bool ShowReferenceButton { get; set; }

        public bool ShowBuyButton { get; set; }

        public bool ShowSellButton { get; set; }

        public bool ShowBillButton { get; set; }

        public bool ShowDeleteButton { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query =
                from car in _context.Cars.Include(x => x.MarkAndModel)
                where id == car.CarId

                join stateRef in _context.CarStateRefId
                on car.CarId equals stateRef.CarId into selectedStateRef
                from stateRef in selectedStateRef.DefaultIfEmpty()

                join expertiseRef in _context.PoolExpertiseReferences
                on car.CarId equals expertiseRef.CarId into seletedExpRef
                from expertiseRef in seletedExpRef.DefaultIfEmpty()

                join worker in _context.Workers
                on expertiseRef.WorkerId equals worker.WorkerId into selectedWorker
                from worker in selectedWorker.DefaultIfEmpty()

                let isBuyerExist = _context.ClientBuyers.Any(x => x.CarId == car.CarId)
                let isSellerExist = _context.ClientSellers.Any(x => x.CarId == car.CarId)

                select new
                {
                    car, stateRef,
                    expertiseRefData = expertiseRef==null ? null : new ExpertiseRefOutput
                    {
                        PoolExpertiseReference = expertiseRef,
                        WorkerName = $"{worker.Lastname} {worker.Firstname[0]}.",
                    },
                    isBuyerExist, isSellerExist
                };

            var queryData = await query.AsNoTracking().FirstOrDefaultAsync();

            if(queryData==null)
            {
                return NotFound();
            }

            var isAuthorize = await _authorizationService
               .AuthorizeAsync(User, queryData.car, Operations.Details);

            if (!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            CarStateRef = queryData.stateRef;
            CarData = queryData.car;
            ExpertiseRefData = queryData.expertiseRefData;

            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            var isWokerExist = await _context.WorkerUsers
                .AnyAsync(x => user.Id == x.IdentityUserId);

            var authorizeEditRef = await _authorizationService
                .AuthorizeAsync(User, queryData.stateRef, Operations.Update);

            var authorizeEditCar = await _authorizationService
                .AuthorizeAsync(User, queryData.car, Operations.Update);

            ShowCarEditButton = authorizeEditCar.Succeeded;
            ShowExpertiseButton = isWokerExist || (queryData.expertiseRefData != null);
            ShowReferenceButton = ((queryData.stateRef == null) && isWokerExist) || authorizeEditRef.Succeeded;
            ShowSellButton = (isWokerExist || queryData.isBuyerExist) && (queryData.car.SellingPrice != null);
            ShowBillButton = queryData.isBuyerExist && (queryData.car.SellingPrice != null);

            ShowBuyButton =
                (isWokerExist || queryData.isSellerExist) &&
                (queryData.stateRef != null) &&
                (queryData.car.BuyingPrice != null);

            ShowDeleteButton = User.IsInRole(Constants.AdministratorRole);

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var car = new Car { CarId = id };
            _context.Cars.Attach(car);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Index");
        }
    }
}
