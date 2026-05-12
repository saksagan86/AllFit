using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseOpeningstijden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "openingstijden",
                table: "Sportscholen");

            migrationBuilder.AddColumn<int>(
                name: "Sportschoolid",
                table: "Faciliteiten",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Openingstijden",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tijdOpen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    tijdSluit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportschoolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Openingstijden", x => x.id);
                    table.ForeignKey(
                        name: "FK_Openingstijden_Sportscholen_SportschoolId",
                        column: x => x.SportschoolId,
                        principalTable: "Sportscholen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Faciliteiten_Sportschoolid",
                table: "Faciliteiten",
                column: "Sportschoolid");

            migrationBuilder.CreateIndex(
                name: "IX_Openingstijden_SportschoolId",
                table: "Openingstijden",
                column: "SportschoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Faciliteiten_Sportscholen_Sportschoolid",
                table: "Faciliteiten",
                column: "Sportschoolid",
                principalTable: "Sportscholen",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faciliteiten_Sportscholen_Sportschoolid",
                table: "Faciliteiten");

            migrationBuilder.DropTable(
                name: "Openingstijden");

            migrationBuilder.DropIndex(
                name: "IX_Faciliteiten_Sportschoolid",
                table: "Faciliteiten");

            migrationBuilder.DropColumn(
                name: "Sportschoolid",
                table: "Faciliteiten");

            migrationBuilder.AddColumn<string>(
                name: "openingstijden",
                table: "Sportscholen",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
