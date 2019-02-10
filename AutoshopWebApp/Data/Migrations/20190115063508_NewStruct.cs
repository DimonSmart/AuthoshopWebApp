using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoshopWebApp.Data.Migrations
{
    public partial class NewStruct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MarkAndModels",
                table: "MarkAndModels");

            migrationBuilder.RenameTable(
                name: "MarkAndModels",
                newName: "MarkAndModel");

            migrationBuilder.RenameColumn(
                name: "MarkAndModelID",
                table: "Cars",
                newName: "MarkAndModelId");

            migrationBuilder.AlterColumn<string>(
                name: "PartName",
                table: "SpareParts",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarkAndModel",
                table: "MarkAndModel",
                column: "MarkAndModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_MarkAndModelId",
                table: "SpareParts",
                column: "MarkAndModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_MarkAndModelId",
                table: "Cars",
                column: "MarkAndModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_MarkAndModel_MarkAndModelId",
                table: "Cars",
                column: "MarkAndModelId",
                principalTable: "MarkAndModel",
                principalColumn: "MarkAndModelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_MarkAndModel_MarkAndModelId",
                table: "SpareParts",
                column: "MarkAndModelId",
                principalTable: "MarkAndModel",
                principalColumn: "MarkAndModelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_MarkAndModel_MarkAndModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_MarkAndModel_MarkAndModelId",
                table: "SpareParts");

            migrationBuilder.DropIndex(
                name: "IX_SpareParts_MarkAndModelId",
                table: "SpareParts");

            migrationBuilder.DropIndex(
                name: "IX_Cars_MarkAndModelId",
                table: "Cars");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarkAndModel",
                table: "MarkAndModel");

            migrationBuilder.RenameTable(
                name: "MarkAndModel",
                newName: "MarkAndModels");

            migrationBuilder.RenameColumn(
                name: "MarkAndModelId",
                table: "Cars",
                newName: "MarkAndModelID");

            migrationBuilder.AlterColumn<string>(
                name: "PartName",
                table: "SpareParts",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarkAndModels",
                table: "MarkAndModels",
                column: "MarkAndModelId");
        }
    }
}
