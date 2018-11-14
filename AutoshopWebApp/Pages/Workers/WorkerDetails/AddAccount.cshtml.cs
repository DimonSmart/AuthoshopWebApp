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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public class AddAccountModel : PageModel, IWorkerPage
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AddAccountModel(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public class AccountInputModel
        {

            [Required]
            [DataType(DataType.EmailAddress)]
            [Display(Name = "Электронная почта")]
            public string Email { get; set; }

            [Display(Name = "Пароль")]
            [DataType(DataType.Password)]
            [StringLength(100, ErrorMessage = "Пароль должен быть не длиннее 100 символов")]
            [Required]
            public string Password { get; set; }

            [Display(Name = "Повторите пароль")]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
            public string Password2 { get; set; }

            [Required]
            [Display(Name = "Роль")]
            public string RoleId { get; set; }

            public int WorkerId { get; set; }
        }

        [BindProperty]
        public AccountInputModel Account { get; set; }

        public IWorkerCrossPageData WorkerCrossPageData { get; set; }

        public List<SelectListItem> RoleSelectList { get; set; }

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

            var pageResult = await LoadPageData(id.Value);

            if (WorkerCrossPageData == null)
            {
                return NotFound();
            }

            Account = new AccountInputModel
            {
                WorkerId = WorkerCrossPageData.WorkerID
            };

            return pageResult;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var isAuthorize = User.IsInRole(Constants.AdministratorRole);

            if (!isAuthorize)
            {
                return new ChallengeResult();
            }

            WorkerCrossPageData = await WorkerCrossPage.FindWorkerDataById(_context, Account.WorkerId);

            if (!ModelState.IsValid)
            {
                return await LoadPageData(Account.WorkerId);
            }

            var role = await _roleManager.FindByIdAsync(Account.RoleId);

            if(role==null)
            {
                
                return await LoadPageData(Account.WorkerId);
            }

            var user = new IdentityUser
            {
                UserName = Account.Email,
                Email = Account.Email,
            };

            var createUserStatus = await _userManager.CreateAsync(user, Account.Password);
            if(!createUserStatus.Succeeded)
            {
                foreach(var err in createUserStatus.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
                
                return await LoadPageData(Account.WorkerId);
            }

            var addToRoleStatus = await _userManager.AddToRoleAsync(user, role.Name);

            if (!addToRoleStatus.Succeeded)
            {
                foreach (var err in addToRoleStatus.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }

                return await LoadPageData(Account.WorkerId);
            }

            await _context.AddUserToWorkerAsync(Account.WorkerId, user.Id);

            return RedirectToPage("./EditAccount", new { id = Account.WorkerId });
        }

        private async Task<PageResult> LoadPageData(int id)
        {
            WorkerCrossPageData = await WorkerCrossPage.FindWorkerDataById(_context, id);

            RoleSelectList = await
                (from role in _roleManager.Roles
                 select new SelectListItem
                 {
                     Text = role.Name,
                     Value = role.Id
                 }).AsNoTracking().ToListAsync();

            return Page();
        }
    }
}