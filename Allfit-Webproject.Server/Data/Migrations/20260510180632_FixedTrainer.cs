using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedTrainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "specialisatie",
                table: "Gebruiker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainerId",
                table: "Aanbod",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Aanbod_TrainerId",
                table: "Aanbod",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Aanbod_Gebruiker_TrainerId",
                table: "Aanbod",
                column: "TrainerId",
                principalTable: "Gebruiker",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aanbod_Gebruiker_TrainerId",
                table: "Aanbod");

            migrationBuilder.DropIndex(
                name: "IX_Aanbod_TrainerId",
                table: "Aanbod");

            migrationBuilder.DropColumn(
                name: "specialisatie",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Aanbod");
        }
    }
}
