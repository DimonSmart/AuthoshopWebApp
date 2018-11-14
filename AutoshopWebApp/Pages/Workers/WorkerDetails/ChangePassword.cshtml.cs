using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Authorization;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models.ForShow;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public class ChangePasswordModel : PageModel, IWorkerPage
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ApplicationDbContext _context;

        public ChangePasswordModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public class InputPasswordModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Повторите пароль")]
            [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
            public string Password2 { get; set; }
        }

        [BindProperty]
        public InputPasswordModel InputModel { get; set; }

        [BindProperty]
        public WorkerCrossPage WorkerData { get; set; }

        public IWorkerCrossPageData WorkerCrossPageData => WorkerData;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var isAuthorize = User.IsInRole(Constants.AdministratorRole);

            if(!isAuthorize)
            {
                return new ChallengeResult();
            }

            return await RedisplayPage(id.Value);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return await RedisplayPage(WorkerData.WorkerID);
            }

            var isAuthorize = User.IsInRole(Constants.AdministratorRole);

            if (!isAuthorize)
            {
                return new ChallengeResult();
            }

            var user = await _context
                .FindUserByWorkerIdAsync(WorkerData.WorkerID);

            if(user==null)
            {
                return NotFound();
            }

            await _userManager
                .RemovePasswordAsync(user);

            var result = await _userManager
                .AddPasswordAsync(user, InputModel.Password);

            if(!result.Succeeded)
            {
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
                return await RedisplayPage(WorkerData.WorkerID);
            }

            return RedirectToPage("EditAccount", new { id = WorkerData.WorkerID });
        }

        private async Task<PageResult> RedisplayPage(int id)
        {
            WorkerData = await WorkerCrossPage.FindWorkerDataById(_context, id);
            return Page();
        }
    }
}