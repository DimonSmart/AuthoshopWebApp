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

namespace AutoshopWebApp.Pages.Admin
{
    public class AdminChangePasswordModel : AdminBasePage
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AdminChangePasswordModel(
            ApplicationDbContext context, 
            IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager) :
            base(context, authorizationService, userManager)
        {
            _signInManager = signInManager;
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

        public string UserName { get; set; }

        [BindProperty]
        public InputPasswordModel PasswordModel { get; set; }

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

            if (user == null)
            {
                return NotFound($"This user not found");
            }

            UserName = user.UserName;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!TryValidateModel(PasswordModel))
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
                return NotFound($"This user not found");
            }


            var changePassResult = await UserManager
                .ChangePasswordAsync(user, PasswordModel.OldPassword, PasswordModel.NewPassword);

            if (!changePassResult.Succeeded)
            {
                foreach (var err in changePassResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Пароль успешно изменён";

            return RedirectToPage();
        }
    }
}