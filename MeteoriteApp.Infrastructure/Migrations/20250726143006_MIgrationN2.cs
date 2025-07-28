using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeteoriteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MIgrationN2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeoLocationId",
                table: "T_Meteorites");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GeoLocationId",
                table: "T_Meteorites",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
