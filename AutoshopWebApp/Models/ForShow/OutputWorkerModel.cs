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
        public Street Street { get; set; }
        public Position Position { get; set; }
        public string Firstname => Worker.Firstname;
        public string Lastname => Worker.Lastname;
        public string Patronymic => Worker.Patronymic;
        public int WorkerID => Worker.WorkerId;

        public static IQueryable<OutputWorkerModel> GetQuery(ApplicationDbContext context)
        {
            return
                from worker in context.Workers
                join street in context.Streets on worker.StreetId equals street.StreetId
                join position in context.Positions on worker.PositionId equals position.PositionId
                select new OutputWorkerModel
                {
                    Worker = worker,
                    Street = street,
                    Position = position
                };
        }
    }
}
