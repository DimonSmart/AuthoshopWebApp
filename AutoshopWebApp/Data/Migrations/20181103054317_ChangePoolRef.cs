using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoshopWebApp.Data.Migrations
{
    public partial class ChangePoolRef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PoolExpertiseReferences_Cars_CarId",
                table: "PoolExpertiseReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_PoolExpertiseReferences_Workers_WorkerId",
                table: "PoolExpertiseReferences");

            migrationBuilder.DropIndex(
                name: "IX_PoolExpertiseReferences_CarId",
                table: "PoolExpertiseReferences");

            migrationBuilder.DropIndex(
                name: "IX_PoolExpertiseReferences_WorkerId",
                table: "PoolExpertiseReferences");

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "TransactionOrders",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "PoolExpertiseReferences",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientPatronymic",
                table: "PoolExpertiseReferences",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientLastname",
                table: "PoolExpertiseReferences",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientFirstname",
                table: "PoolExpertiseReferences",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "PoolExpertiseReferences",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "TransactionOrders",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "WorkerId",
                table: "PoolExpertiseReferences",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ClientPatronymic",
                table: "PoolExpertiseReferences",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ClientLastname",
                table: "PoolExpertiseReferences",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ClientFirstname",
                table: "PoolExpertiseReferences",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "CarId",
                table: "PoolExpertiseReferences",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_PoolExpertiseReferences_CarId",
                table: "PoolExpertiseReferences",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolExpertiseReferences_WorkerId",
                table: "PoolExpertiseReferences",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PoolExpertiseReferences_Cars_CarId",
                table: "PoolExpertiseReferences",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PoolExpertiseReferences_Workers_WorkerId",
                table: "PoolExpertiseReferences",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
