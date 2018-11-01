﻿// <auto-generated />
using System;
using AutoshopWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AutoshopWebApp.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181101053508_ChangePoolRef")]
    partial class ChangePoolRef
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AutoshopWebApp.Models.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BodyNumber")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<decimal?>("BuyingPrice");

                    b.Property<string>("ChassisNumber")
                        .HasMaxLength(100);

                    b.Property<string>("Color")
                        .IsRequired();

                    b.Property<string>("EngineNumber")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("MarkAndModelID");

                    b.Property<string>("RegNumber")
                        .HasMaxLength(10);

                    b.Property<DateTime>("ReleaseDate");

                    b.Property<decimal?>("ReleasePrice");

                    b.Property<int>("Run");

                    b.Property<int>("SaleStatus");

                    b.Property<decimal?>("SellingPrice");

                    b.HasKey("CarId");

                    b.ToTable("Cars");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.CarStateRef", b =>
                {
                    b.Property<int>("CarStateRefId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarId");

                    b.Property<string>("Expert")
                        .HasMaxLength(100);

                    b.Property<decimal>("ExpertisePrice");

                    b.Property<DateTime>("ReferenceDate");

                    b.Property<string>("ReferenceNumber")
                        .HasMaxLength(100);

                    b.HasKey("CarStateRefId");

                    b.ToTable("CarStateRefId");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.ClientBuyer", b =>
                {
                    b.Property<int>("ClientBuyerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountNumber")
                        .HasMaxLength(50);

                    b.Property<int>("ApartmentNumber");

                    b.Property<DateTime>("BornDate");

                    b.Property<DateTime>("BuyDate");

                    b.Property<int>("CarId");

                    b.Property<string>("Firstname")
                        .HasMaxLength(100);

                    b.Property<int>("HouseNumber");

                    b.Property<string>("LastName")
                        .HasMaxLength(100);

                    b.Property<string>("PasNumber")
                        .HasMaxLength(50);

                    b.Property<string>("Patronymic")
                        .HasMaxLength(100);

                    b.Property<int>("PaymentTypeId");

                    b.Property<int>("StreetId");

                    b.HasKey("ClientBuyerId");

                    b.ToTable("ClientBuyers");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.ClientSeller", b =>
                {
                    b.Property<int>("ClientSellerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApartmentNumber");

                    b.Property<DateTime>("BornDate");

                    b.Property<int>("CarId");

                    b.Property<string>("DocName")
                        .HasMaxLength(100);

                    b.Property<string>("DocNumber")
                        .HasMaxLength(50);

                    b.Property<string>("Firstname")
                        .HasMaxLength(100);

                    b.Property<int>("HouseNumber");

                    b.Property<DateTime>("IssueDate");

                    b.Property<string>("IssuedBy")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .HasMaxLength(100);

                    b.Property<string>("PasNumber")
                        .HasMaxLength(50);

                    b.Property<string>("Patronymic")
                        .HasMaxLength(100);

                    b.Property<DateTime>("SellingDate");

                    b.Property<int>("StreetId");

                    b.HasKey("ClientSellerId");

                    b.ToTable("ClientSellers");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.MarkAndModel", b =>
                {
                    b.Property<int>("MarkAndModelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarMark")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("CarModel")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("MarkAndModelId");

                    b.ToTable("MarkAndModels");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PaymentTypeName")
                        .HasMaxLength(100);

                    b.HasKey("PaymentTypeId");

                    b.ToTable("PaymentTypes");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.PoolExpertiseReference", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CarId");

                    b.Property<string>("ClientFirstname")
                        .IsRequired();

                    b.Property<string>("ClientLastname")
                        .IsRequired();

                    b.Property<string>("ClientPatronymic")
                        .IsRequired();

                    b.Property<DateTime>("IssueDate");

                    b.Property<int?>("WorkerId");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("WorkerId");

                    b.ToTable("PoolExpertiseReferences");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.Position", b =>
                {
                    b.Property<int>("PositionId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PositionName")
                        .HasMaxLength(100);

                    b.Property<decimal>("Salary");

                    b.HasKey("PositionId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.SparePart", b =>
                {
                    b.Property<int>("SparePartId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MarkAndModelId");

                    b.Property<int>("PartCount");

                    b.Property<string>("PartName")
                        .HasMaxLength(100);

                    b.Property<decimal>("PartPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("SparePartId");

                    b.ToTable("SpareParts");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.Street", b =>
                {
                    b.Property<int>("StreetId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StreetName")
                        .HasMaxLength(100);

                    b.HasKey("StreetId");

                    b.ToTable("Streets");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.TransactionOrder", b =>
                {
                    b.Property<int>("TransactionOrderId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("OrderDate");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("PositionId");

                    b.Property<string>("Reason")
                        .HasMaxLength(200);

                    b.Property<int>("WorkerId");

                    b.HasKey("TransactionOrderId");

                    b.ToTable("TransactionOrders");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.Worker", b =>
                {
                    b.Property<int>("WorkerId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApartmentNumber");

                    b.Property<DateTime>("BornDate");

                    b.Property<string>("Firstname")
                        .HasMaxLength(100);

                    b.Property<int>("HouseNumber");

                    b.Property<string>("Lastname")
                        .HasMaxLength(100);

                    b.Property<string>("Patronymic")
                        .HasMaxLength(100);

                    b.Property<int>("PositionId");

                    b.Property<int>("StreetId");

                    b.HasKey("WorkerId");

                    b.ToTable("Workers");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.WorkerUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserID");

                    b.Property<int>("WorkerID");

                    b.HasKey("Id");

                    b.ToTable("WorkerUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("AutoshopWebApp.Models.PoolExpertiseReference", b =>
                {
                    b.HasOne("AutoshopWebApp.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId");

                    b.HasOne("AutoshopWebApp.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
