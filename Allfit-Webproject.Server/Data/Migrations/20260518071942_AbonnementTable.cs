using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AbonnementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abonnement",
                columns: table => new
                {
                    LidID = table.Column<int>(type: "int", nullable: false),
                    MollieID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LidmaatschapID = table.Column<int>(type: "int", nullable: false),
                    DateStart = table.Column<DateOnly>(type: "date", nullable: false),
                    DateEnd = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abonnement", x => new { x.LidID, x.MollieID });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abonnement");
        }
    }
}
