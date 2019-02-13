using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Authorization;
using AutoshopWebApp.Models;
using AutoshopWebApp.Models.ForShow;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public class EditAccountModel : PageModel, IWorkerPage
    {

        private readonly AutoshopWebApp.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditAccountModel(
            AutoshopWebApp.Data.ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public class AccountData:IWorkerCrossPageData
        {
            public string Firstname { get; set; }

            public string Lastname { get; set; }

            public string Patronymic { get; set; }

            public int WorkerID { get; set; }

            public string UserID { get; set; }

            [Display(Name = "Электронная почта")]
            public string Login { get; set; }

            [Display(Name = "Уровень доступа")]
            public string Role { get; set; }
        }

        public AccountData PageData { get; set; }

        public IWorkerCrossPageData WorkerCrossPageData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.AdministratorRole);

            if(!isAuthorized)
            {
                return new ChallengeResult();
            }

            PageData = await
                (from worker in _context.Workers
                 join workerUser in _context.WorkerUsers 
                 on worker.WorkerId equals workerUser.WorkerId
                 where worker.WorkerId == id
                 select new AccountData
                 {
                     Firstname = worker.Firstname,
                     Lastname = worker.Lastname,
                     Patronymic = worker.Patronymic,
                     WorkerID = worker.WorkerId,
                     UserID = workerUser.IdentityUserId,
                 }).AsNoTracking().FirstOrDefaultAsync();

            if(PageData == null)
            {
                WorkerCrossPageData = await WorkerCrossPage.FindWorkerDataById(_context, id.Value);
            }
            else
            {
                var user = await _userManager.FindByIdAsync(PageData.UserID);
                var roles = await _userManager.GetRolesAsync(user);
                PageData.Login = user.UserName;
                PageData.Role = roles.Count==0 ? "Not exist" : roles[0];
                WorkerCrossPageData = PageData;
            }

            if(WorkerCrossPageData == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAccountAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.AdministratorRole);

            if (!isAuthorized)
            {
                return new ChallengeResult();
            }

            var workerUser = await _context.WorkerUsers
                .FirstOrDefaultAsync(item => item.WorkerId == id);

            if(workerUser==null)
            {
                return NotFound();
            }

            var user = await _context.FindUserByWorkerIdAsync(id.Value);

            if(user==null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);

            _context.WorkerUsers.Remove(workerUser);

            await _context.SaveChangesAsync();

            return RedirectToPage("EditAccount", new { id });
        }

        public async Task<IActionResult> OnPostBindAccountAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.AdministratorRole);

            if (!isAuthorized)
            {
                return new ChallengeResult();
            }

            var user = await _userManager.GetUserAsync(User);

            if(user==null)
            {
                return NotFound();
            }

            var workerUser = await _context.WorkerUsers
                .FirstOrDefaultAsync(x => x.IdentityUserId == user.Id);

            if(workerUser!=null)
            {
                _context.Remove(workerUser);
            }

            workerUser = new WorkerUser
            {
                IdentityUserId = user.Id,
                WorkerId = id.Value,
            };

            await _context.WorkerUsers.AddAsync(workerUser);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}