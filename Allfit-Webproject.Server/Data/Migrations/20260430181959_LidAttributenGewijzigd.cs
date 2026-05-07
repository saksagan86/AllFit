using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class LidAttributenGewijzigd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "adres",
                table: "Gebruiker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "huisnummer",
                table: "Gebruiker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "postcode",
                table: "Gebruiker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "woonplaats",
                table: "Gebruiker",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "adres",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "huisnummer",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "postcode",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "woonplaats",
                table: "Gebruiker");
        }
    }
}
