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

namespace AutoshopWebApp.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService _authorizationService;

        public CreateModel(ApplicationDbContext context, IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Car CarData { get; set; }

        public List<SelectListItem> CarStatusSelectList { get; set; }


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

            var carModel = await _context
                .AddMarkAndModelAsync(CarData.MarkAndModel.CarMark, CarData.MarkAndModel.CarModel);

            CarData.MarkAndModel = carModel;

            _context.Cars.Add(CarData);

            await _context.SaveChangesAsync();

            return RedirectToPage("./CarDetails/Index", new { id = CarData.CarId });
        }
    }
}