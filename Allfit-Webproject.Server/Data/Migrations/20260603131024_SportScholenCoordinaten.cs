using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class SportScholenCoordinaten : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "lat",
                table: "Sportscholen",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "lon",
                table: "Sportscholen",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lat",
                table: "Sportscholen");

            migrationBuilder.DropColumn(
                name: "lon",
                table: "Sportscholen");
        }
    }
}
