using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class VoedingsAdvies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doelen",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    actief = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doelen", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "GebruikerDoelen",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gebruikerId = table.Column<int>(type: "int", nullable: false),
                    doelId = table.Column<int>(type: "int", nullable: false),
                    aangemaaktOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    gewijzigdOp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GebruikerDoelen", x => x.id);
                    table.ForeignKey(
                        name: "FK_GebruikerDoelen_Doelen_doelId",
                        column: x => x.doelId,
                        principalTable: "Doelen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GebruikerDoelen_Gebruiker_gebruikerId",
                        column: x => x.gebruikerId,
                        principalTable: "Gebruiker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Voedingsschemas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doelId = table.Column<int>(type: "int", nullable: false),
                    titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voedingsschemas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Voedingsschemas_Doelen_doelId",
                        column: x => x.doelId,
                        principalTable: "Doelen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoedingsschemaRegels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    voedingsschemaId = table.Column<int>(type: "int", nullable: false),
                    maaltijdMoment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    advies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    volgorde = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoedingsschemaRegels", x => x.id);
                    table.ForeignKey(
                        name: "FK_VoedingsschemaRegels_Voedingsschemas_voedingsschemaId",
                        column: x => x.voedingsschemaId,
                        principalTable: "Voedingsschemas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GebruikerDoelen_doelId",
                table: "GebruikerDoelen",
                column: "doelId");

            migrationBuilder.CreateIndex(
                name: "IX_GebruikerDoelen_gebruikerId",
                table: "GebruikerDoelen",
                column: "gebruikerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoedingsschemaRegels_voedingsschemaId",
                table: "VoedingsschemaRegels",
                column: "voedingsschemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Voedingsschemas_doelId",
                table: "Voedingsschemas",
                column: "doelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GebruikerDoelen");

            migrationBuilder.DropTable(
                name: "VoedingsschemaRegels");

            migrationBuilder.DropTable(
                name: "Voedingsschemas");

            migrationBuilder.DropTable(
                name: "Doelen");
        }
    }
}
