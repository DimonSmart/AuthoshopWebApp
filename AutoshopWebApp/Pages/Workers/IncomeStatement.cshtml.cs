using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoshopWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AutoshopWebApp.Pages.Workers
{
    public class IncomeStatementModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IncomeStatementModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? month, int? year)
        {
            if(month==null || year == null)
            {
                return NotFound();
            }

            var querySelling =
                from seller in _context.ClientSellers
                join sellingCar in _context.Cars
                on seller.CarId equals sellingCar.CarId
                where sellingCar.BuyingPrice != null
                group sellingCar.BuyingPrice.Value
                by new { seller.SellingDate.Month, seller.SellingDate.Year}
                into sellingByMonth
                select new { sellingByMonth.Key, loss = sellingByMonth.Sum() };

            var queryBuying =
                from buyer in _context.ClientBuyers
                join buyingCar in _context.Cars
                on buyer.CarId equals buyingCar.CarId
                where buyingCar.SellingPrice != null
                group buyingCar.SellingPrice.Value
                by new { buyer.BuyDate.Month, buyer.BuyDate.Year }
                into buyingByMonth
                select new { buyingByMonth.Key, profit = buyingByMonth.Sum() };

            var queryLeftJoin =
                from sellData in querySelling
                join buyData in queryBuying
                on sellData.Key equals buyData.Key 
                into buyQueryData
                from buyData in buyQueryData.DefaultIfEmpty()
                select new { sellData, buyData };

            var queryRightJoin =
                from buyData  in queryBuying 
                join sellData in querySelling
                on buyData.Key equals sellData.Key
                into sellQueryData
                from sellData in sellQueryData.DefaultIfEmpty()
                select new { sellData, buyData };

            var mainQuery =
                from data in queryLeftJoin.Union(queryRightJoin)
                let profit = data.buyData == null ? 0 : data.buyData.profit
                let loss = data.sellData == null ? 0 : data.sellData.loss
                select new
                {
                    date = data.buyData == null ? data.sellData.Key : data.buyData.Key,
                    profit,
                    loss,
                    sum = profit - loss,
                };

            var queryData = await mainQuery.ToListAsync();


            return Page();
        }
    }
}