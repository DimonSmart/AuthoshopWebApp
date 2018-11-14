using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoshopWebApp.Data.Migrations
{
    public partial class ChangeCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SaleStatus",
                table: "Cars");

            migrationBuilder.AlterColumn<decimal>(
                name: "ExpertisePrice",
                table: "CarStateRefId",
                type: "decimal(16,2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<decimal>(
                name: "SellingPrice",
                table: "Cars",
                type: "decimal(16,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReleasePrice",
                table: "Cars",
                type: "decimal(16,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyingPrice",
                table: "Cars",
                type: "decimal(16,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "ExpertisePrice",
                table: "CarStateRefId",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "SellingPrice",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ReleasePrice",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyingPrice",
                table: "Cars",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SaleStatus",
                table: "Cars",
                nullable: false,
                defaultValue: 0);
        }
    }
}
