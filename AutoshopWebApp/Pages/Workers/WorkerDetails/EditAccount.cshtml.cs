using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public EditAccountModel(
            AutoshopWebApp.Data.ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public class AccountData:IWorkerCrossPageData
        {
            public string Firstname { get; set; }

            public string Lastname { get; set; }

            public string Patronymic { get; set; }

            public int WorkerID { get; set; }

            public string UserID { get; set; }

            public string Login { get; set; }

            
        }

        public AccountData PageData { get; set; }

        public IWorkerCrossPageData WorkerCrossPageData { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }

            var pageData = await
                (from worker in _context.Workers
                 join workerUser in _context.WorkerUsers on worker.WorkerId equals workerUser.WorkerID
                 where worker.WorkerId == id
                 select new AccountData
                 {
                     Firstname = worker.Firstname,
                     Lastname = worker.Lastname,
                     Patronymic = worker.Patronymic,
                     WorkerID = worker.WorkerId,
                     UserID = workerUser.UserID,
                 }).AsNoTracking().FirstOrDefaultAsync();

            if(pageData == null)
            {
                WorkerCrossPageData = await WorkerCrossPage.FindWorkerByIdAsync(_context, id.Value);
            }
            else
            {
                pageData.Login = (await _userManager.FindByIdAsync(pageData.UserID)).UserName;
                WorkerCrossPageData = pageData;
            }

            return Page();
        }
    }
}