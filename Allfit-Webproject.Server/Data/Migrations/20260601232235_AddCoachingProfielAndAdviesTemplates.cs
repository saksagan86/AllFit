using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCoachingProfielAndAdviesTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdviesTemplates",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doelId = table.Column<int>(type: "int", nullable: false),
                    activiteitniveau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bmiCategorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    calorieAdvies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    eiwitAdvies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    algemeneTips = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    actief = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdviesTemplates", x => x.id);
                    table.ForeignKey(
                        name: "FK_AdviesTemplates_Doelen_doelId",
                        column: x => x.doelId,
                        principalTable: "Doelen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GebruikerCoachingProfielen",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gebruikerId = table.Column<int>(type: "int", nullable: false),
                    doelId = table.Column<int>(type: "int", nullable: false),
                    leeftijd = table.Column<int>(type: "int", nullable: false),
                    lengteCm = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    gewichtKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    activiteitniveau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    doelTermijnMaanden = table.Column<int>(type: "int", nullable: false),
                    bmi = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    bmiCategorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adviesTemplateId = table.Column<int>(type: "int", nullable: true),
                    aangemaaktOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gewijzigdOp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GebruikerCoachingProfielen", x => x.id);
                    table.ForeignKey(
                        name: "FK_GebruikerCoachingProfielen_AdviesTemplates_adviesTemplateId",
                        column: x => x.adviesTemplateId,
                        principalTable: "AdviesTemplates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_GebruikerCoachingProfielen_Doelen_doelId",
                        column: x => x.doelId,
                        principalTable: "Doelen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GebruikerCoachingProfielen_Gebruiker_gebruikerId",
                        column: x => x.gebruikerId,
                        principalTable: "Gebruiker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WekelijkseVoortgangen",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gebruikerCoachingProfielId = table.Column<int>(type: "int", nullable: false),
                    weekStartDatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gewichtKg = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    afgerondeTrainingen = table.Column<int>(type: "int", nullable: false),
                    weekDoel = table.Column<int>(type: "int", nullable: false),
                    doelBehaald = table.Column<bool>(type: "bit", nullable: false),
                    notitie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    aangemaaktOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gewijzigdOp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WekelijkseVoortgangen", x => x.id);
                    table.ForeignKey(
                        name: "FK_WekelijkseVoortgangen_GebruikerCoachingProfielen_gebruikerCoachingProfielId",
                        column: x => x.gebruikerCoachingProfielId,
                        principalTable: "GebruikerCoachingProfielen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdviesTemplates_doelId",
                table: "AdviesTemplates",
                column: "doelId");

            migrationBuilder.CreateIndex(
                name: "IX_GebruikerCoachingProfielen_adviesTemplateId",
                table: "GebruikerCoachingProfielen",
                column: "adviesTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_GebruikerCoachingProfielen_doelId",
                table: "GebruikerCoachingProfielen",
                column: "doelId");

            migrationBuilder.CreateIndex(
                name: "IX_GebruikerCoachingProfielen_gebruikerId",
                table: "GebruikerCoachingProfielen",
                column: "gebruikerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WekelijkseVoortgangen_gebruikerCoachingProfielId_weekStartDatum",
                table: "WekelijkseVoortgangen",
                columns: new[] { "gebruikerCoachingProfielId", "weekStartDatum" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WekelijkseVoortgangen");

            migrationBuilder.DropTable(
                name: "GebruikerCoachingProfielen");

            migrationBuilder.DropTable(
                name: "AdviesTemplates");
        }
    }
}
