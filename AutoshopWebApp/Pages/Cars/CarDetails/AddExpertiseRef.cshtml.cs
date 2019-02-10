using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class AddExpertiseRefModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public AddExpertiseRefModel(ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var queryData = await
                (from car in _context.Cars
                 select new
                 {
                     car.CarId,
                     car.MarkAndModel
                 }).FirstOrDefaultAsync();

            if(queryData==null)
            {
                return NotFound();
            }

            CarId = queryData.CarId;
            MarkAndModel = queryData.MarkAndModel;

            return Page();
        }

        public int CarId { get; set; }

        [BindProperty]
        public CarStateRef CarStateRef { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = await _authorizationService
                .AuthorizeAsync(User, CarStateRef, Operations.Create);

            if(!isAuthorized.Succeeded)
            {
                return new ChallengeResult();
            }

            _context.CarStateRefId.Add(CarStateRef);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = CarStateRef.CarId });
        }
    }
}