using AutoshopWebApp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models.ForShow
{
    public class OutputWorkerModel : IWorkerCrossPageData
    {
        public Worker Worker { get; set; }
        public string Firstname => Worker.Firstname;
        public string Lastname => Worker.Lastname;
        public string Patronymic => Worker.Patronymic;
        public int WorkerID => Worker.WorkerId;

        public static IQueryable<OutputWorkerModel> GetQuery(ApplicationDbContext context)
        {
            return
                from worker in context.Workers
                .Include(x => x.Street)
                .Include(x => x.Position)
                select new OutputWorkerModel
                {
                    Worker = worker
                };
        }
    }
}
