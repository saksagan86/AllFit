using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMissing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "startGewichtKg",
                table: "GebruikerCoachingProfielen",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "doelGewichtKg",
                table: "GebruikerCoachingProfielen",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "startDatum",
                table: "GebruikerCoachingProfielen",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "eindDatum",
                table: "GebruikerCoachingProfielen",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "doelAfgerond",
                table: "GebruikerCoachingProfielen",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "doelBehaald",
                table: "GebruikerCoachingProfielen",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "evaluatieTekst",
                table: "GebruikerCoachingProfielen",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "startGewichtKg",
                table: "GebruikerCoachingProfielen");

            migrationBuilder.DropColumn(
                name: "doelGewichtKg",
                table: "GebruikerCoachingProfielen");

            migrationBuilder.DropColumn(
                name: "startDatum",
                table: "GebruikerCoachingProfielen");

            migrationBuilder.DropColumn(
                name: "eindDatum",
                table: "GebruikerCoachingProfielen");

            migrationBuilder.DropColumn(
                name: "doelAfgerond",
                table: "GebruikerCoachingProfielen");

            migrationBuilder.DropColumn(
                name: "doelBehaald",
                table: "GebruikerCoachingProfielen");

            migrationBuilder.DropColumn(
                name: "evaluatieTekst",
                table: "GebruikerCoachingProfielen");
        }
    }
}
