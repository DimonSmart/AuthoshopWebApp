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

        [BindProperty]
        public MarkAndModel MarkAndModelData { get; set; }

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

            var carModel = await
                (from model in _context.MarkAndModels
                 where (model.CarMark.Equals(MarkAndModelData.CarMark, StringComparison.OrdinalIgnoreCase))
                 && (model.CarModel.Equals(MarkAndModelData.CarModel, StringComparison.OrdinalIgnoreCase))
                 select model).FirstOrDefaultAsync();

            if(carModel == null)
            {
                carModel = MarkAndModelData;
                await _context.AddAsync(carModel);
                await _context.SaveChangesAsync();
            }

            CarData.MarkAndModelID = carModel.MarkAndModelId;
                
            _context.Cars.Add(CarData);
            await _context.SaveChangesAsync();

            return RedirectToPage("./CarDetails/Index", new { id = CarData.CarId });
        }
    }
}