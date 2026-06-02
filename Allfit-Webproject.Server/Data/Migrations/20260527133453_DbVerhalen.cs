using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class DbVerhalen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Verhalen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inhoud = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeplaatstOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LidId = table.Column<int>(type: "int", nullable: false),
                    AanbodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verhalen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Verhalen_Aanbod_AanbodId",
                        column: x => x.AanbodId,
                        principalTable: "Aanbod",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Verhalen_Gebruiker_LidId",
                        column: x => x.LidId,
                        principalTable: "Gebruiker",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Verhalen_AanbodId",
                table: "Verhalen",
                column: "AanbodId");

            migrationBuilder.CreateIndex(
                name: "IX_Verhalen_LidId",
                table: "Verhalen",
                column: "LidId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Verhalen");
        }
    }
}
