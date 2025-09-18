using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class AjoutRelationProtocoleDossier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DossierId",
                table: "Protocoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Protocoles_DossierId",
                table: "Protocoles",
                column: "DossierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Protocoles_Dossiers_DossierId",
                table: "Protocoles",
                column: "DossierId",
                principalTable: "Dossiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Protocoles_Dossiers_DossierId",
                table: "Protocoles");

            migrationBuilder.DropIndex(
                name: "IX_Protocoles_DossierId",
                table: "Protocoles");

            migrationBuilder.DropColumn(
                name: "DossierId",
                table: "Protocoles");
        }
    }
}
