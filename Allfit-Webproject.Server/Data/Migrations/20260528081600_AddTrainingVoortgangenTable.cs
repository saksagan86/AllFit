using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Allfit_Webproject.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingVoortgangenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TrainingVoortgangen",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    gebruikerDoelId = table.Column<int>(type: "int", nullable: false),
                    trainingsDag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    afgerondOp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    notitie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingVoortgangen", x => x.id);
                    table.ForeignKey(
                        name: "FK_TrainingVoortgangen_GebruikerDoelen_gebruikerDoelId",
                        column: x => x.gebruikerDoelId,
                        principalTable: "GebruikerDoelen",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingVoortgangen_gebruikerDoelId",
                table: "TrainingVoortgangen",
                column: "gebruikerDoelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingVoortgangen");
        }
    }
}
