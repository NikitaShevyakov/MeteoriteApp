using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeteoriteApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MigrationN3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_T_Meteorites_Name",
                table: "T_Meteorites",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_T_Meteorites_Name",
                table: "T_Meteorites");
        }
    }
}
