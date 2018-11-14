using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class EditCarModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public EditCarModel(ApplicationDbContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public Car Car { get; set; }

        [BindProperty]
        public MarkAndModel MarkAndModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await
                (from car in _context.Cars
                 where car.CarId == id
                 join mark in _context.MarkAndModels
                 on car.MarkAndModelID equals mark.MarkAndModelId
                 select new { car, mark }).FirstOrDefaultAsync();

            if(query==null)
            {
                return NotFound();
            }

            var isAuthorize = await _authorizationService
               .AuthorizeAsync(User, query.car, Operations.Update);

            if (!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            Car = query.car;
            MarkAndModel = query.mark;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorize = await _authorizationService
               .AuthorizeAsync(User, Car, Operations.Update);

            if (!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            var mark = await _context
                .AddMarkAndModelAsync(MarkAndModel.CarMark, MarkAndModel.CarModel);

            Car.MarkAndModelID = mark.MarkAndModelId;

            _context.Attach(Car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(Car.CarId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { id = Car.CarId });
        }

        private bool CarExists(int id)
        {
            return _context.Cars.Any(e => e.CarId == id);
        }
    }
}
