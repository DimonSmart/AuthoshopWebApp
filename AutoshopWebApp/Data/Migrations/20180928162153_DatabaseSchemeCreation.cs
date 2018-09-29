using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoshopWebApp.Data.Migrations
{
    public partial class DatabaseSchemeCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarReferences",
                columns: table => new
                {
                    CarReferenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ReferenceNumber = table.Column<string>(maxLength: 100, nullable: true),
                    ReferenceDate = table.Column<DateTime>(nullable: false),
                    Expert = table.Column<string>(maxLength: 100, nullable: true),
                    ExpertisePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarReferences", x => x.CarReferenceId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MarkAndModelID = table.Column<int>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    EngineNumber = table.Column<string>(maxLength: 100, nullable: true),
                    RegNumber = table.Column<string>(maxLength: 100, nullable: true),
                    BodyNumber = table.Column<string>(maxLength: 100, nullable: true),
                    ChassisNumber = table.Column<string>(maxLength: 100, nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Run = table.Column<int>(nullable: false),
                    ReleasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SellingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BuyingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CarReferenceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "ClientBuyers",
                columns: table => new
                {
                    ClientBuyerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Patronymic = table.Column<string>(maxLength: 100, nullable: true),
                    BornDate = table.Column<DateTime>(nullable: false),
                    PasNumber = table.Column<string>(maxLength: 50, nullable: true),
                    StreetId = table.Column<int>(nullable: false),
                    HouseNumber = table.Column<int>(nullable: false),
                    ApartmentNumber = table.Column<int>(nullable: false),
                    CarId = table.Column<int>(nullable: false),
                    BuyDate = table.Column<DateTime>(nullable: false),
                    AccountNumber = table.Column<string>(maxLength: 50, nullable: true),
                    PaymentTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientBuyers", x => x.ClientBuyerId);
                });

            migrationBuilder.CreateTable(
                name: "ClientSellers",
                columns: table => new
                {
                    ClientSellerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: true),
                    Patronymic = table.Column<string>(maxLength: 100, nullable: true),
                    BornDate = table.Column<DateTime>(nullable: false),
                    PasNumber = table.Column<string>(maxLength: 50, nullable: true),
                    StreetId = table.Column<int>(nullable: false),
                    HouseNumber = table.Column<int>(nullable: false),
                    ApartmentNumber = table.Column<int>(nullable: false),
                    CarId = table.Column<int>(nullable: false),
                    SellingDate = table.Column<DateTime>(nullable: false),
                    DocNumber = table.Column<string>(maxLength: 50, nullable: true),
                    DocName = table.Column<string>(maxLength: 100, nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    IssuedBy = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSellers", x => x.ClientSellerId);
                });

            migrationBuilder.CreateTable(
                name: "MarkAndModels",
                columns: table => new
                {
                    MarkAndModelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarMark = table.Column<string>(maxLength: 100, nullable: false),
                    CarModel = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarkAndModels", x => x.MarkAndModelId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    PaymentTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PaymentTypeName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PaymentTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PositionName = table.Column<string>(maxLength: 100, nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    SparePartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MarkAndModelId = table.Column<int>(nullable: false),
                    PartPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartCount = table.Column<int>(nullable: false),
                    PartName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.SparePartId);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    StreetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.StreetId);
                });

            migrationBuilder.CreateTable(
                name: "TransactionOrders",
                columns: table => new
                {
                    TransactionOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkerId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(maxLength: 200, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 50, nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionOrders", x => x.TransactionOrderId);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    WorkerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firstname = table.Column<string>(maxLength: 100, nullable: true),
                    Lastname = table.Column<string>(maxLength: 100, nullable: true),
                    Patronymic = table.Column<string>(maxLength: 100, nullable: true),
                    BornDate = table.Column<DateTime>(nullable: false),
                    StreetId = table.Column<int>(nullable: false),
                    HouseNumber = table.Column<int>(nullable: false),
                    ApartmentNumber = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.WorkerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarReferences");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "ClientBuyers");

            migrationBuilder.DropTable(
                name: "ClientSellers");

            migrationBuilder.DropTable(
                name: "MarkAndModels");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropTable(
                name: "Streets");

            migrationBuilder.DropTable(
                name: "TransactionOrders");

            migrationBuilder.DropTable(
                name: "Workers");
        }
    }
}
