using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Data;
using AutoshopWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Cars.CarDetails
{
    public class CustomerBillModel : PageModel
    {
        private readonly ApplicationDbContext _context;


        public CustomerBillModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billQuery =
                from car in _context.Cars
                where car.CarId == id
                join markAndModel in _context.MarkAndModels
                on car.MarkAndModelID equals markAndModel.MarkAndModelId
                join clientBuyer in _context.ClientBuyers
                on car.CarId equals clientBuyer.CarId
                join paymentType in _context.PaymentTypes
                on clientBuyer.PaymentTypeId equals paymentType.PaymentTypeId
                let dueDate = clientBuyer.BuyDate.AddDays(5)
                select new
                {
                    car, markAndModel, paymentType, dueDate,
                    clientBuyer = new ClientBuyer
                    {
                        Firstname = clientBuyer.Firstname,
                        LastName = clientBuyer.LastName,
                        Patronymic = clientBuyer.Patronymic,
                        BuyDate = clientBuyer.BuyDate,
                        ApartmentNumber = clientBuyer.ApartmentNumber
                    }
                };

            var billData = await billQuery
                .AsNoTracking().FirstOrDefaultAsync();

            if(billData==null)
            {
                return NotFound();
            }

            Car = billData.car;
            MarkAndModel = billData.markAndModel;
            ClientBuyer = billData.clientBuyer;
            DueDate = billData.dueDate;
            PaymentType = billData.paymentType;

            return Page();
        }

        public Car Car { get; set; }

        public MarkAndModel MarkAndModel { get; set; }

        public PaymentType PaymentType { get; set; }

        public ClientBuyer ClientBuyer { get; set; }

        [Display(Name = "Срок оплаты")]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

    }
}