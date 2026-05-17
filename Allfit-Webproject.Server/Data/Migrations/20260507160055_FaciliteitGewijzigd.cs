using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class FaciliteitGewijzigd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faciliteiten_Sportscholen_Sportschoolid",
                table: "Faciliteiten");

            migrationBuilder.RenameColumn(
                name: "Sportschoolid",
                table: "Faciliteiten",
                newName: "SportschoolId");

            migrationBuilder.RenameIndex(
                name: "IX_Faciliteiten_Sportschoolid",
                table: "Faciliteiten",
                newName: "IX_Faciliteiten_SportschoolId");

            migrationBuilder.AlterColumn<int>(
                name: "SportschoolId",
                table: "Faciliteiten",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Faciliteiten_Sportscholen_SportschoolId",
                table: "Faciliteiten",
                column: "SportschoolId",
                principalTable: "Sportscholen",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faciliteiten_Sportscholen_SportschoolId",
                table: "Faciliteiten");

            migrationBuilder.RenameColumn(
                name: "SportschoolId",
                table: "Faciliteiten",
                newName: "Sportschoolid");

            migrationBuilder.RenameIndex(
                name: "IX_Faciliteiten_SportschoolId",
                table: "Faciliteiten",
                newName: "IX_Faciliteiten_Sportschoolid");

            migrationBuilder.AlterColumn<int>(
                name: "Sportschoolid",
                table: "Faciliteiten",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Faciliteiten_Sportscholen_Sportschoolid",
                table: "Faciliteiten",
                column: "Sportschoolid",
                principalTable: "Sportscholen",
                principalColumn: "id");
        }
    }
}
