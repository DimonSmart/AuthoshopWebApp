using AutoshopWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models.ForShow
{
    public interface IWorkerPage
    {
        [BindProperty]
        IWorkerCrossPageData WorkerCrossPageData { get; }
    }

    public class WorkerCrossPage:IWorkerCrossPageData
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }
        public int WorkerID { get; set; }

        public static async Task<WorkerCrossPage> FindWorkerByIdAsync(ApplicationDbContext context, int id)
        {
            return await
                (from worker in context.Workers
                where worker.WorkerId == id
                select new WorkerCrossPage
                {
                    Firstname = worker.Firstname,
                    Lastname = worker.Lastname,
                    Patronymic = worker.Patronymic,
                    WorkerID = worker.WorkerId
                }).AsNoTracking().FirstOrDefaultAsync();
        }
    }

    public interface IWorkerCrossPageData
    {
        string Firstname { get; }
        string Lastname { get; }
        string Patronymic { get; }
        int WorkerID { get; }
    }
}
