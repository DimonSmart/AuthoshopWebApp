using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoshopWebApp.Data.Migrations
{
    public partial class ChangeMarkAndModelStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_MarkAndModel_MarkAndModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_MarkAndModel_MarkAndModelId",
                table: "SpareParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarkAndModel",
                table: "MarkAndModel");

            migrationBuilder.RenameTable(
                name: "MarkAndModel",
                newName: "MarkAndModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarkAndModels",
                table: "MarkAndModels",
                column: "MarkAndModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_MarkAndModels_MarkAndModelId",
                table: "Cars",
                column: "MarkAndModelId",
                principalTable: "MarkAndModels",
                principalColumn: "MarkAndModelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpareParts_MarkAndModels_MarkAndModelId",
                table: "SpareParts",
                column: "MarkAndModelId",
                principalTable: "MarkAndModels",
                principalColumn: "MarkAndModelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_MarkAndModels_MarkAndModelId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_SpareParts_MarkAndModels_MarkAndModelId",
                table: "SpareParts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MarkAndModels",
                table: "MarkAndModels");

            migrationBuilder.RenameTable(
                name: "MarkAndModels",
                newName: "MarkAndModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MarkAndModel",
                table: "MarkAndModel",
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
    }
}
