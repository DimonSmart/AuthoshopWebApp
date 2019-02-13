using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class WorkerUser
    {
        public int Id { get; set; }

        public Worker Worker { get; set; }
        public int WorkerId { get; set; }

        public IdentityUser IdentityUser { get; set; }
        public string IdentityUserId { get; set; }
    }
}
