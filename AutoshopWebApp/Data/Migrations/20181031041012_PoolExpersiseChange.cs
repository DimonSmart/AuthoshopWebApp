using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoshopWebApp.Data.Migrations
{
    public partial class PoolExpersiseChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegNumber",
                table: "Cars",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "PoolExpertiseReferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientFirstname = table.Column<string>(nullable: true),
                    ClientLastname = table.Column<string>(nullable: true),
                    ClientPatronymic = table.Column<string>(nullable: true),
                    CarId = table.Column<int>(nullable: true),
                    WorkerId = table.Column<int>(nullable: true),
                    IssueDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoolExpertiseReferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoolExpertiseReferences_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PoolExpertiseReferences_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PoolExpertiseReferences_CarId",
                table: "PoolExpertiseReferences",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolExpertiseReferences_WorkerId",
                table: "PoolExpertiseReferences",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PoolExpertiseReferences");

            migrationBuilder.AlterColumn<string>(
                name: "RegNumber",
                table: "Cars",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);
        }
    }
}
