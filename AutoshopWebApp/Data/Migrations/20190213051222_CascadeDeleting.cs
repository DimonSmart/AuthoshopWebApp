using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoshopWebApp.Data.Migrations
{
    public partial class CascadeDeleting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkerID",
                table: "WorkerUsers",
                newName: "WorkerId");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "WorkerUsers",
                newName: "IdentityUserId");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityUserId",
                table: "WorkerUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkerUsers_IdentityUserId",
                table: "WorkerUsers",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerUsers_WorkerId",
                table: "WorkerUsers",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_PositionId",
                table: "Workers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_StreetId",
                table: "Workers",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionOrders_PositionId",
                table: "TransactionOrders",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionOrders_WorkerId",
                table: "TransactionOrders",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolExpertiseReferences_CarId",
                table: "PoolExpertiseReferences",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolExpertiseReferences_WorkerId",
                table: "PoolExpertiseReferences",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSellers_CarId",
                table: "ClientSellers",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSellers_StreetId",
                table: "ClientSellers",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientSellers_WorkerId",
                table: "ClientSellers",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBuyers_CarId",
                table: "ClientBuyers",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBuyers_PaymentTypeId",
                table: "ClientBuyers",
                column: "PaymentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBuyers_StreetId",
                table: "ClientBuyers",
                column: "StreetId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientBuyers_WorkerId",
                table: "ClientBuyers",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_CarStateRefId_CarId",
                table: "CarStateRefId",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarStateRefId_Cars_CarId",
                table: "CarStateRefId",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientBuyers_Cars_CarId",
                table: "ClientBuyers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientBuyers_PaymentTypes_PaymentTypeId",
                table: "ClientBuyers",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "PaymentTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientBuyers_Streets_StreetId",
                table: "ClientBuyers",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "StreetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientBuyers_Workers_WorkerId",
                table: "ClientBuyers",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSellers_Cars_CarId",
                table: "ClientSellers",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSellers_Streets_StreetId",
                table: "ClientSellers",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "StreetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientSellers_Workers_WorkerId",
                table: "ClientSellers",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PoolExpertiseReferences_Cars_CarId",
                table: "PoolExpertiseReferences",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "CarId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PoolExpertiseReferences_Workers_WorkerId",
                table: "PoolExpertiseReferences",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionOrders_Positions_PositionId",
                table: "TransactionOrders",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "PositionId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionOrders_Workers_WorkerId",
                table: "TransactionOrders",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Positions_PositionId",
                table: "Workers",
                column: "PositionId",
                principalTable: "Positions",
                principalColumn: "PositionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workers_Streets_StreetId",
                table: "Workers",
                column: "StreetId",
                principalTable: "Streets",
                principalColumn: "StreetId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerUsers_AspNetUsers_IdentityUserId",
                table: "WorkerUsers",
                column: "IdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkerUsers_Workers_WorkerId",
                table: "WorkerUsers",
                column: "WorkerId",
                principalTable: "Workers",
                principalColumn: "WorkerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarStateRefId_Cars_CarId",
                table: "CarStateRefId");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientBuyers_Cars_CarId",
                table: "ClientBuyers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientBuyers_PaymentTypes_PaymentTypeId",
                table: "ClientBuyers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientBuyers_Streets_StreetId",
                table: "ClientBuyers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientBuyers_Workers_WorkerId",
                table: "ClientBuyers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientSellers_Cars_CarId",
                table: "ClientSellers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientSellers_Streets_StreetId",
                table: "ClientSellers");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientSellers_Workers_WorkerId",
                table: "ClientSellers");

            migrationBuilder.DropForeignKey(
                name: "FK_PoolExpertiseReferences_Cars_CarId",
                table: "PoolExpertiseReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_PoolExpertiseReferences_Workers_WorkerId",
                table: "PoolExpertiseReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionOrders_Positions_PositionId",
                table: "TransactionOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionOrders_Workers_WorkerId",
                table: "TransactionOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Positions_PositionId",
                table: "Workers");

            migrationBuilder.DropForeignKey(
                name: "FK_Workers_Streets_StreetId",
                table: "Workers");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerUsers_AspNetUsers_IdentityUserId",
                table: "WorkerUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkerUsers_Workers_WorkerId",
                table: "WorkerUsers");

            migrationBuilder.DropIndex(
                name: "IX_WorkerUsers_IdentityUserId",
                table: "WorkerUsers");

            migrationBuilder.DropIndex(
                name: "IX_WorkerUsers_WorkerId",
                table: "WorkerUsers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_PositionId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_Workers_StreetId",
                table: "Workers");

            migrationBuilder.DropIndex(
                name: "IX_TransactionOrders_PositionId",
                table: "TransactionOrders");

            migrationBuilder.DropIndex(
                name: "IX_TransactionOrders_WorkerId",
                table: "TransactionOrders");

            migrationBuilder.DropIndex(
                name: "IX_PoolExpertiseReferences_CarId",
                table: "PoolExpertiseReferences");

            migrationBuilder.DropIndex(
                name: "IX_PoolExpertiseReferences_WorkerId",
                table: "PoolExpertiseReferences");

            migrationBuilder.DropIndex(
                name: "IX_ClientSellers_CarId",
                table: "ClientSellers");

            migrationBuilder.DropIndex(
                name: "IX_ClientSellers_StreetId",
                table: "ClientSellers");

            migrationBuilder.DropIndex(
                name: "IX_ClientSellers_WorkerId",
                table: "ClientSellers");

            migrationBuilder.DropIndex(
                name: "IX_ClientBuyers_CarId",
                table: "ClientBuyers");

            migrationBuilder.DropIndex(
                name: "IX_ClientBuyers_PaymentTypeId",
                table: "ClientBuyers");

            migrationBuilder.DropIndex(
                name: "IX_ClientBuyers_StreetId",
                table: "ClientBuyers");

            migrationBuilder.DropIndex(
                name: "IX_ClientBuyers_WorkerId",
                table: "ClientBuyers");

            migrationBuilder.DropIndex(
                name: "IX_CarStateRefId_CarId",
                table: "CarStateRefId");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "WorkerUsers",
                newName: "WorkerID");

            migrationBuilder.RenameColumn(
                name: "IdentityUserId",
                table: "WorkerUsers",
                newName: "UserID");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "WorkerUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
