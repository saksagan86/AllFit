using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class LesEnInschrijvingen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lessen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tijd = table.Column<TimeSpan>(type: "time", nullable: false),
                    MaxDeelnemers = table.Column<int>(type: "int", nullable: false),
                    AanbodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessen_Aanbod_AanbodId",
                        column: x => x.AanbodId,
                        principalTable: "Aanbod",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inschrijvingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LidId = table.Column<int>(type: "int", nullable: false),
                    LesId = table.Column<int>(type: "int", nullable: false),
                    ExtraBegeleiding = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inschrijvingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inschrijvingen_Gebruiker_LidId",
                        column: x => x.LidId,
                        principalTable: "Gebruiker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inschrijvingen_Lessen_LesId",
                        column: x => x.LesId,
                        principalTable: "Lessen",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_LesId",
                table: "Inschrijvingen",
                column: "LesId");

            migrationBuilder.CreateIndex(
                name: "IX_Inschrijvingen_LidId",
                table: "Inschrijvingen",
                column: "LidId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessen_AanbodId",
                table: "Lessen",
                column: "AanbodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inschrijvingen");

            migrationBuilder.DropTable(
                name: "Lessen");
        }
    }
}
