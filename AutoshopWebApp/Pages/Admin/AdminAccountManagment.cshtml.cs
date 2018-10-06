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
    public class AdminAccountManagmentModel : AdminBasePage
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminAccountManagmentModel(
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

        public class InputPasswordModel
        {
            [Display(Name = "Старый пароль")]
            [DataType(DataType.Password)]
            [Required]
            public string OldPassword { get; set; }

            [Display(Name = "Новый пароль")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "Пароль должен быть не длиннее 100 символов")]
            [Required]
            public string NewPassword { get; set; }

            [Display(Name = "Повторите пароль")]
            [DataType(DataType.Password)]
            [Compare("NewPassword", ErrorMessage = "Пароли должны совпадать")]
            public string NewPassword2 { get; set; }
        }

        [BindProperty]
        public InputLoginModel LoginModel { get; set; }

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

        public async Task<IActionResult> OnPostLoginAsync()
        {
            if(!TryValidateModel(LoginModel))
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

            return RedirectToPage();
        }
    }
}