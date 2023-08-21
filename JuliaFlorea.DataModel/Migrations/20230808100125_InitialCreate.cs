using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JuliaFlorea.DataModel.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<string>(nullable: false),
                    CountryName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "DrugTypes",
                columns: table => new
                {
                    DrugTypeId = table.Column<string>(nullable: false),
                    DrugTypeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugTypes", x => x.DrugTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Depots",
                columns: table => new
                {
                    DepotId = table.Column<string>(nullable: false),
                    DepotName = table.Column<string>(nullable: false),
                    DepotAddress = table.Column<string>(nullable: true),
                    StorageCapacity = table.Column<int>(nullable: false),
                    CountryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depots", x => x.DepotId);
                    table.ForeignKey(
                        name: "FK_Depots_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sites",
                columns: table => new
                {
                    SiteId = table.Column<string>(nullable: false),
                    SiteName = table.Column<string>(nullable: false),
                    CountryCode = table.Column<string>(nullable: true),
                
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sites", x => x.SiteId);
                    table.ForeignKey(
                        name: "FK_Sites_Countries_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "Countries",
                        principalColumn: "CountryCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DrugUnits",
                columns: table => new
                {
                    DrugUnitId = table.Column<string>(nullable: false),
                    PickNumber = table.Column<int>(nullable: false),
                    ManufacturingDate = table.Column<DateTime>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false),
                    Manufacturer = table.Column<string>(nullable: true),
                    DepotId = table.Column<string>(nullable: true),
                    DrugTypeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugUnits", x => x.DrugUnitId);
                    table.ForeignKey(
                        name: "FK_DrugUnits_Depots_DepotId",
                        column: x => x.DepotId,
                        principalTable: "Depots",
                        principalColumn: "DepotId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DrugUnits_DrugTypes_DrugTypeId",
                        column: x => x.DrugTypeId,
                        principalTable: "DrugTypes",
                        principalColumn: "DrugTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Depots_CountryId",
                table: "Depots",
                column: "CountryId",
                unique: true,
                filter: "[CountryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DrugUnits_DepotId",
                table: "DrugUnits",
                column: "DepotId");

            migrationBuilder.CreateIndex(
                name: "IX_DrugUnits_DrugTypeId",
                table: "DrugUnits",
                column: "DrugTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sites_CountryCode",
                table: "Sites",
                column: "CountryCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugUnits");

            migrationBuilder.DropTable(
                name: "Sites");

            migrationBuilder.DropTable(
                name: "Depots");

            migrationBuilder.DropTable(
                name: "DrugTypes");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
