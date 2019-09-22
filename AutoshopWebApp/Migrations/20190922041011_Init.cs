using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AutoshopWebApp.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarkAndModels",
                columns: table => new
                {
                    MarkAndModelId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PositionName = table.Column<string>(maxLength: 100, nullable: true),
                    Salary = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Streets",
                columns: table => new
                {
                    StreetId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    StreetName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streets", x => x.StreetId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MarkAndModelId = table.Column<int>(nullable: false),
                    Color = table.Column<string>(nullable: false),
                    EngineNumber = table.Column<string>(maxLength: 100, nullable: false),
                    BodyNumber = table.Column<string>(maxLength: 100, nullable: false),
                    ChassisNumber = table.Column<string>(maxLength: 100, nullable: true),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    RegNumber = table.Column<string>(maxLength: 10, nullable: true),
                    Run = table.Column<int>(nullable: false),
                    ReleasePrice = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    SellingPrice = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    BuyingPrice = table.Column<decimal>(type: "decimal(16,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                    table.ForeignKey(
                        name: "FK_Cars_MarkAndModels_MarkAndModelId",
                        column: x => x.MarkAndModelId,
                        principalTable: "MarkAndModels",
                        principalColumn: "MarkAndModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpareParts",
                columns: table => new
                {
                    SparePartId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    MarkAndModelId = table.Column<int>(nullable: false),
                    PartPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PartCount = table.Column<int>(nullable: false),
                    PartName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpareParts", x => x.SparePartId);
                    table.ForeignKey(
                        name: "FK_SpareParts_MarkAndModels_MarkAndModelId",
                        column: x => x.MarkAndModelId,
                        principalTable: "MarkAndModels",
                        principalColumn: "MarkAndModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    WorkerId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    table.ForeignKey(
                        name: "FK_Workers_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Workers_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CarStateRefId",
                columns: table => new
                {
                    CarStateRefId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CarId = table.Column<int>(nullable: false),
                    ReferenceNumber = table.Column<string>(maxLength: 100, nullable: true),
                    ReferenceDate = table.Column<DateTime>(nullable: false),
                    Expert = table.Column<string>(maxLength: 100, nullable: true),
                    ExpertisePrice = table.Column<decimal>(type: "decimal(16,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarStateRefId", x => x.CarStateRefId);
                    table.ForeignKey(
                        name: "FK_CarStateRefId_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientBuyers",
                columns: table => new
                {
                    ClientBuyerId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Firstname = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Patronymic = table.Column<string>(maxLength: 100, nullable: false),
                    BornDate = table.Column<DateTime>(nullable: false),
                    PasNumber = table.Column<string>(maxLength: 50, nullable: false),
                    PasIssueBy = table.Column<string>(maxLength: 100, nullable: false),
                    PasIssueDate = table.Column<DateTime>(nullable: false),
                    StreetId = table.Column<int>(nullable: false),
                    HouseNumber = table.Column<int>(nullable: false),
                    ApartmentNumber = table.Column<int>(nullable: false),
                    CarId = table.Column<int>(nullable: false),
                    BuyDate = table.Column<DateTime>(nullable: false),
                    AccountNumber = table.Column<string>(maxLength: 50, nullable: true),
                    PaymentTypeId = table.Column<int>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientBuyers", x => x.ClientBuyerId);
                    table.ForeignKey(
                        name: "FK_ClientBuyers_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientBuyers_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "PaymentTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientBuyers_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientBuyers_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientSellers",
                columns: table => new
                {
                    ClientSellerId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Firstname = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    Patronymic = table.Column<string>(maxLength: 100, nullable: false),
                    BornDate = table.Column<DateTime>(nullable: false),
                    PasNumber = table.Column<string>(maxLength: 50, nullable: false),
                    PasIssueDate = table.Column<DateTime>(nullable: false),
                    PasIssueBy = table.Column<string>(maxLength: 100, nullable: false),
                    StreetId = table.Column<int>(nullable: false),
                    HouseNumber = table.Column<int>(nullable: false),
                    ApartmentNumber = table.Column<int>(nullable: false),
                    CarId = table.Column<int>(nullable: false),
                    SellingDate = table.Column<DateTime>(nullable: false),
                    DocNumber = table.Column<string>(maxLength: 50, nullable: false),
                    DocName = table.Column<string>(maxLength: 100, nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    OwnDocIssuedBy = table.Column<string>(maxLength: 100, nullable: false),
                    WorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSellers", x => x.ClientSellerId);
                    table.ForeignKey(
                        name: "FK_ClientSellers_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientSellers_Streets_StreetId",
                        column: x => x.StreetId,
                        principalTable: "Streets",
                        principalColumn: "StreetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientSellers_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PoolExpertiseReferences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ClientFirstname = table.Column<string>(nullable: false),
                    ClientLastname = table.Column<string>(nullable: false),
                    ClientPatronymic = table.Column<string>(nullable: false),
                    CarId = table.Column<int>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PoolExpertiseReferences_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionOrders",
                columns: table => new
                {
                    TransactionOrderId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    WorkerId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(maxLength: 200, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 50, nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionOrders", x => x.TransactionOrderId);
                    table.ForeignKey(
                        name: "FK_TransactionOrders_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionOrders_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerUsers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    WorkerId = table.Column<int>(nullable: false),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerUsers_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkerUsers_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "WorkerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_MarkAndModelId",
                table: "Cars",
                column: "MarkAndModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CarStateRefId_CarId",
                table: "CarStateRefId",
                column: "CarId");

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
                name: "IX_PoolExpertiseReferences_CarId",
                table: "PoolExpertiseReferences",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_PoolExpertiseReferences_WorkerId",
                table: "PoolExpertiseReferences",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_SpareParts_MarkAndModelId",
                table: "SpareParts",
                column: "MarkAndModelId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionOrders_PositionId",
                table: "TransactionOrders",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionOrders_WorkerId",
                table: "TransactionOrders",
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
                name: "IX_WorkerUsers_IdentityUserId",
                table: "WorkerUsers",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerUsers_WorkerId",
                table: "WorkerUsers",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CarStateRefId");

            migrationBuilder.DropTable(
                name: "ClientBuyers");

            migrationBuilder.DropTable(
                name: "ClientSellers");

            migrationBuilder.DropTable(
                name: "PoolExpertiseReferences");

            migrationBuilder.DropTable(
                name: "SpareParts");

            migrationBuilder.DropTable(
                name: "TransactionOrders");

            migrationBuilder.DropTable(
                name: "WorkerUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "MarkAndModels");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Streets");
        }
    }
}
