using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBOpzet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aanbod",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    extraBegeleiding = table.Column<bool>(type: "bit", nullable: false),
                    beschrijvingBegeleiding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    niveau = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    duur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    doelgroep = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aanbod", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ContactFormulieren",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactFormulieren", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Faciliteiten",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faciliteiten", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Gebruiker",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    wacthwoord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    telefoonnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    geboortedatum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActief = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruiker", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Sportscholen",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    openingstijden = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sportscholen", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aanbod");

            migrationBuilder.DropTable(
                name: "ContactFormulieren");

            migrationBuilder.DropTable(
                name: "Faciliteiten");

            migrationBuilder.DropTable(
                name: "Gebruiker");

            migrationBuilder.DropTable(
                name: "Sportscholen");
        }
    }
}
