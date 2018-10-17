using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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

            WorkerCrossPageData = await WorkerCrossPage.FindWorkerByIdAsync(_context, id.Value);

            if(WorkerCrossPageData == null)
            {
                return NotFound();
            }

            Account = new AccountInputModel
            {
                WorkerId = WorkerCrossPageData.WorkerID
            };

            RoleSelectList = await
                (from role in _roleManager.Roles
                 select new SelectListItem
                 {
                     Text = role.Name,
                     Value = role.Id
                 }).AsNoTracking().ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var role = await _roleManager.FindByIdAsync(Account.RoleId);

            if(role==null)
            {
                return Page();
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
                return Page();
            }

            var addToRoleStatus = await _userManager.AddToRoleAsync(user, role.Name);

            if (!addToRoleStatus.Succeeded)
            {
                foreach (var err in addToRoleStatus.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
                return Page();
            }

            await _context.AddUserToWorkerAsync(Account.WorkerId, user.Id);

            return RedirectToPage("./EditAccount", new { id = Account.WorkerId });
        }
    }
}