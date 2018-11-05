using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoshopWebApp.Data.Migrations
{
    public partial class AddClientsPasIssueDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PasIssueDate",
                table: "ClientSellers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PasIssueDate",
                table: "ClientBuyers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasIssueDate",
                table: "ClientSellers");

            migrationBuilder.DropColumn(
                name: "PasIssueDate",
                table: "ClientBuyers");
        }
    }
}
