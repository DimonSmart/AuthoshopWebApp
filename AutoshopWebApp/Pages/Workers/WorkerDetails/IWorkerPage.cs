using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Pages.Workers.WorkerDetails
{
    public interface IWorkerPage
    {
        [BindProperty]
        IWorkerCrossPageData WorkerCrossPage { get; }
    }

    public class WorkerCrossPage:IWorkerCrossPageData
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Patronymic { get; set; }

        public int WorkerID { get; set; }
    }

    public interface IWorkerCrossPageData
    {
        string Firstname { get; set; }
        string Lastname { get; set; }
        string Patronymic { get; set; }
        int WorkerID { get; set; }
    }
}
