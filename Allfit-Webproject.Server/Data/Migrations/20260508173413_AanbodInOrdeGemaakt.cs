using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AanbodInOrdeGemaakt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "extraBegeleiding",
                table: "Aanbod",
                newName: "ExtraBegeleiding");

            migrationBuilder.RenameColumn(
                name: "beschrijvingBegeleiding",
                table: "Aanbod",
                newName: "BeschrijvingBegeleiding");

            migrationBuilder.AlterColumn<string>(
                name: "BeschrijvingBegeleiding",
                table: "Aanbod",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "SportschoolId",
                table: "Aanbod",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SportschoolId",
                table: "Aanbod");

            migrationBuilder.RenameColumn(
                name: "ExtraBegeleiding",
                table: "Aanbod",
                newName: "extraBegeleiding");

            migrationBuilder.RenameColumn(
                name: "BeschrijvingBegeleiding",
                table: "Aanbod",
                newName: "beschrijvingBegeleiding");

            migrationBuilder.AlterColumn<string>(
                name: "beschrijvingBegeleiding",
                table: "Aanbod",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
