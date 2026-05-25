using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddProeflesRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sportschool",
                table: "Proefles",
                newName: "SportschoolID");

            migrationBuilder.RenameColumn(
                name: "Les",
                table: "Proefles",
                newName: "LesID");

            migrationBuilder.CreateIndex(
                name: "IX_Proefles_LesID",
                table: "Proefles",
                column: "LesID");

            migrationBuilder.CreateIndex(
                name: "IX_Proefles_SportschoolID",
                table: "Proefles",
                column: "SportschoolID");

            migrationBuilder.AddForeignKey(
                name: "FK_Proefles_Aanbod_LesID",
                table: "Proefles",
                column: "LesID",
                principalTable: "Aanbod",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Proefles_Sportscholen_SportschoolID",
                table: "Proefles",
                column: "SportschoolID",
                principalTable: "Sportscholen",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proefles_Aanbod_LesID",
                table: "Proefles");

            migrationBuilder.DropForeignKey(
                name: "FK_Proefles_Sportscholen_SportschoolID",
                table: "Proefles");

            migrationBuilder.DropIndex(
                name: "IX_Proefles_LesID",
                table: "Proefles");

            migrationBuilder.DropIndex(
                name: "IX_Proefles_SportschoolID",
                table: "Proefles");

            migrationBuilder.RenameColumn(
                name: "SportschoolID",
                table: "Proefles",
                newName: "Sportschool");

            migrationBuilder.RenameColumn(
                name: "LesID",
                table: "Proefles",
                newName: "Les");
        }
    }
}
