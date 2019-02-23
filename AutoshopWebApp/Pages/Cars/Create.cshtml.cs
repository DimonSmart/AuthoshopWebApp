using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using AutoshopWebApp.Models.ForShow;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using AutoshopWebApp.Authorization;
using AutoshopWebApp.Services;

namespace AutoshopWebApp.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ICarService _carService;

        public CreateModel(IAuthorizationService authorizationService, ICarService carService)
        {
            _authorizationService = authorizationService;
            _carService = carService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Car CarData { get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorize = await _authorizationService
                .AuthorizeAsync(User, CarData, Operations.Create);

            if(!isAuthorize.Succeeded)
            {
                return new ChallengeResult();
            }

            await _carService.CreateAsync(CarData);

            return RedirectToPage("./CarDetails/Index", new { id = CarData.CarId });
        }
    }
}