using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeteoriteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MIgrationN1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_MeteoriteCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MeteoriteCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_MeteoriteClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MeteoriteClassifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_MeteoriteDiscoveryStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MeteoriteDiscoveryStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_MeteoriteGeolocationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MeteoriteGeolocationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Meteorites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mass = table.Column<double>(type: "float", nullable: false),
                    DateDiscovered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    RawRegionByDistrict = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RawRegionByGeozone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ClassificationId = table.Column<int>(type: "int", nullable: false),
                    DiscoveryStatusId = table.Column<int>(type: "int", nullable: false),
                    GeoLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Meteorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_T_Meteorites_T_MeteoriteCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "T_MeteoriteCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_Meteorites_T_MeteoriteClassifications_ClassificationId",
                        column: x => x.ClassificationId,
                        principalTable: "T_MeteoriteClassifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_Meteorites_T_MeteoriteDiscoveryStatuses_DiscoveryStatusId",
                        column: x => x.DiscoveryStatusId,
                        principalTable: "T_MeteoriteDiscoveryStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "T_MeteoriteGeoLocations",
                columns: table => new
                {
                    MeteoriteId = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_MeteoriteGeoLocations", x => x.MeteoriteId);
                    table.ForeignKey(
                        name: "FK_T_MeteoriteGeoLocations_T_MeteoriteGeolocationTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "T_MeteoriteGeolocationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_T_MeteoriteGeoLocations_T_Meteorites_MeteoriteId",
                        column: x => x.MeteoriteId,
                        principalTable: "T_Meteorites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_T_MeteoriteClassifications_Name",
                table: "T_MeteoriteClassifications",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_T_MeteoriteGeoLocations_TypeId",
                table: "T_MeteoriteGeoLocations",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Meteorites_CategoryId",
                table: "T_Meteorites",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Meteorites_ClassificationId",
                table: "T_Meteorites",
                column: "ClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Meteorites_DiscoveryStatusId",
                table: "T_Meteorites",
                column: "DiscoveryStatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_MeteoriteGeoLocations");

            migrationBuilder.DropTable(
                name: "T_MeteoriteGeolocationTypes");

            migrationBuilder.DropTable(
                name: "T_Meteorites");

            migrationBuilder.DropTable(
                name: "T_MeteoriteCategories");

            migrationBuilder.DropTable(
                name: "T_MeteoriteClassifications");

            migrationBuilder.DropTable(
                name: "T_MeteoriteDiscoveryStatuses");
        }
    }
}
