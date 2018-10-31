using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class PoolExpertiseReference
    {
        public int Id { get; set; }
        public string ClientFirstname { get; set; }
        public string ClientLastname { get; set; }
        public string ClientPatronymic { get; set; }
        public Car Car { get; set; }
        public Worker Worker { get; set; }
        public DateTime IssueDate { get; set; }
    }
}
