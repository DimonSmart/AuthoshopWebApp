using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AutoshopWebApp.Models;
using System.Linq.Expressions;
using System.Linq;

namespace AutoshopWebApp.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { 
        }

        public ApplicationDbContext()
        {
            InitQueries();
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<CarStateRef> CarStateRefId { get; set; }

        public DbSet<ClientBuyer> ClientBuyers { get; set; }

        public DbSet<ClientSeller> ClientSellers { get; set; }

        public DbSet<MarkAndModel> MarkAndModels { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<SparePart> SpareParts { get; set; }

        public DbSet<Street> Streets { get; set; }

        public DbSet<TransactionOrder> TransactionOrders { get; set; }

        public DbSet<Worker> Workers { get; set; }

        public DbSet<WorkerUser> WorkerUsers { get; set; }

        public DbSet<PoolExpertiseReference> PoolExpertiseReferences { get; set; }

        public IQueryable<BuyingOrder> BuyingOrders { get; private set; }
    }
}
