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
    public class ChangeLoginModel : PageModel, IWorkerPage
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

       

        public ChangeLoginModel(
            AutoshopWebApp.Data.ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public class PageInputModel
        {
            public int WorkerId { get; set; }

            [Required]
            [Display(Name = "Электронная почта")]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

            [Display(Name = "Уровень доступа")]
            public string Role { get; set; }
        }

        public IWorkerCrossPageData WorkerCrossPageData { get; set; }

        [BindProperty]
        public PageInputModel InputModel { get; set; }

        public List<SelectListItem> RoleSelectList { get; set; }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            WorkerCrossPageData = await WorkerCrossPage.FindWorkerDataById(_context, id.Value);
            var user = await _context.FindUserByWorkerIdAsync(id.Value);

            if (WorkerCrossPageData == null || user==null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);

            InputModel = new PageInputModel
            {
                Email = user.Email,
                WorkerId = id.Value,
                Role = roles.Count == 0 ? string.Empty : roles[0],
            };

            RoleSelectList = await
                (from role in _roleManager.Roles
                 select new SelectListItem
                 {
                     Value = role.Name,
                     Text = role.Name
                 }).AsNoTracking().ToListAsync();

            InputModel.WorkerId = WorkerCrossPageData.WorkerID;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.FindUserByWorkerIdAsync(InputModel.WorkerId);

            if(user==null)
            {
                return NotFound();
            }

            user.Email = InputModel.Email;
            user.UserName = InputModel.Email;

            var result = await _userManager.UpdateAsync(user);

            if(!ErrorCheck(result))
            {
                return Page();
            }

            if(!await _userManager.IsInRoleAsync(user, InputModel.Role))
            {
                var roles = await _userManager.GetRolesAsync(user);
                result = await _userManager.RemoveFromRolesAsync(user, roles);
                if (!ErrorCheck(result))
                {
                    return Page();
                }

                result = await _userManager.AddToRoleAsync(user, InputModel.Role);

                if (!ErrorCheck(result))
                {
                    return Page();
                }
            }

            return RedirectToPage("EditAccount", new { id = InputModel.WorkerId });
        }

        private bool ErrorCheck(IdentityResult result)
        {
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
                return false;
            }
            return true;
        }
    }
}