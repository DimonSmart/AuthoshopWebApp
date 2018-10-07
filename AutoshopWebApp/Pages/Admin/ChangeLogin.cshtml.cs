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

        public class InputLoginModel
        {
            public string UserID { get; set; }

            [Display(Name = "Электронная почта")]
            [DataType(DataType.EmailAddress)]
            [Required]
            public string Email { get; set; }
        }

        [BindProperty]
        public InputLoginModel LoginModel { get; set; }

        [TempData]
        public string StatusMessage { get; set; } 

        public async Task<IActionResult> OnGetAsync(string id)
        {
            var isAuthorized = User.IsInRole(Constants.AdministratorRole);
            if (!isAuthorized)
            {
                return new ChallengeResult();
            }

            IdentityUser user;

            if(string.IsNullOrEmpty(id))
            {
                user = await UserManager.GetUserAsync(User);
            }
            else
            {
                user = await UserManager.FindByIdAsync(id);
            }
            
            if(user == null)
            {
                return NotFound($"This user not found");
            }

            LoginModel = new InputLoginModel { Email = user.Email, UserID = user.Id };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var user = await UserManager.FindByIdAsync(LoginModel.UserID);

            if (user == null)
            {
                return NotFound($"This user not found");
            }

            var isAuthorized = User.IsInRole(Constants.AdministratorRole);
            if (!isAuthorized)
            {
                return new ChallengeResult();
            }

            user.UserName = LoginModel.Email;
            user.Email = LoginModel.Email;

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