using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Authorization;
using AutoshopWebApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AutoshopWebApp.Pages.Admin
{
    public class AdminChangeLoginModel : AdminBasePage
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminChangeLoginModel(
            ApplicationDbContext context, 
            IAuthorizationService authorizationService, 
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager) 
            : base(context, authorizationService, userManager)
        {
            _signInManager = signInManager;
        }

        [Required]
        [BindProperty]
        [Display(Name = "Электронная почта")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [TempData]
        public string StatusMessage { get; set; } 

        public async Task<IActionResult> OnGetAsync()
        {
            var isAuthorized = User.IsInRole(Constants.AdministratorRole);
            if (!isAuthorized)
            {
                return new ChallengeResult();
            }

            var user = await UserManager.GetUserAsync(User);

            if(user == null)
            {
                return NotFound();
            }

            Email = user.Email;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var isAuthorized = User.IsInRole(Constants.AdministratorRole);

            if (!isAuthorized)
            {
                return new ChallengeResult();
            }

            var user = await UserManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound();
            }

            user.UserName = Email;
            user.Email = Email;

            var changeLogin = await UserManager.UpdateAsync(user);

            if(!changeLogin.Succeeded)
            {
                foreach(var err in changeLogin.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Имя успешно изменено";

            return RedirectToPage();
        }
    }
}