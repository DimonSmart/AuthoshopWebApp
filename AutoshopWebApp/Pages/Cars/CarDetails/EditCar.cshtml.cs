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
using AutoshopWebApp.Services;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class EditCarModel : PageModel
    {
        private readonly ICarService _carService;
        private readonly IAuthorizationService _authorizationService;

        public EditCarModel(ICarService carService,
            IAuthorizationService authorizationService)
        {
            _carService = carService;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _carService.ReadAsync(id.Value);

            if(car==null)
            {
                return NotFound();
            }

            var isAuthorize = await _authorizationService
               .AuthorizeAsync(User, car, Operations.Update);

            if (!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            Car = car;

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

            try
            {
                await _carService.UpdateAsync(Car);
            }
            catch (DbUpdateConcurrencyException)
            { 
                if (!(await _carService.IsExistAsync(Car.CarId)))
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
    }
}
