using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoshopWebApp.Data.Migrations
{
    public partial class ChangeCarDataStruct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarReferences");

            migrationBuilder.DropColumn(
                name: "CarReferenceId",
                table: "Cars");

            migrationBuilder.AlterColumn<decimal>(
                name: "SellingPrice",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReleasePrice",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EngineNumber",
                table: "Cars",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Cars",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyingPrice",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BodyNumber",
                table: "Cars",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaleStatus",
                table: "Cars",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CarStateRefId",
                columns: table => new
                {
                    CarStateRefId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CarId = table.Column<int>(nullable: false),
                    ReferenceNumber = table.Column<string>(maxLength: 100, nullable: true),
                    ReferenceDate = table.Column<DateTime>(nullable: false),
                    Expert = table.Column<string>(maxLength: 100, nullable: true),
                    ExpertisePrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStateRefId", x => x.CarStateRefId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarStateRefId");

            migrationBuilder.DropColumn(
                name: "SaleStatus",
                table: "Cars");

            migrationBuilder.AlterColumn<decimal>(
                name: "SellingPrice",
                table: "Cars",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReleasePrice",
                table: "Cars",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EngineNumber",
                table: "Cars",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyingPrice",
                table: "Cars",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BodyNumber",
                table: "Cars",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CarReferenceId",
                table: "Cars",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CarReferences",
                columns: table => new
                {
                    CarReferenceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Expert = table.Column<string>(maxLength: 100, nullable: true),
                    ExpertisePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReferenceDate = table.Column<DateTime>(nullable: false),
                    ReferenceNumber = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarReferences", x => x.CarReferenceId);
                });
        }
    }
}
