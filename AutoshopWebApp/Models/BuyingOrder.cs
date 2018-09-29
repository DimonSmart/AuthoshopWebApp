using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoshopWebApp.Models
{
    public class BuyingOrder
    {
        public int OrderNumber { get; set; }
        public DateTime SellingDate { get; set; }
        public string LastName { get; set; }
        public string Firstname { get; set; }
        public string Patronymic { get; set; }
        public string PasNumber { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public string CarMark { get; set; }
        public string CarModel { get; set; }
        public string Color { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime ReferenceDate { get; set; }
        public string Expert { get; set; }
        public decimal ExpertisePrice { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal FullPrice => (SellingPrice - ExpertisePrice);
        public string DocName { get; set; }
        public string DocNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public string IssuedBy { get; set; }
        public string BodyNumber { get; set; }
        public string EngineNumber { get; set; }
        public string ChassisNumber { get; set; }
        public int Run { get; set; }
    }
}
