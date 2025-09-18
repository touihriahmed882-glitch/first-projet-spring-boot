using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class AddCureDossierRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DossierId",
                table: "Cures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cures_DossierId",
                table: "Cures",
                column: "DossierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cures_Dossiers_DossierId",
                table: "Cures",
                column: "DossierId",
                principalTable: "Dossiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cures_Dossiers_DossierId",
                table: "Cures");

            migrationBuilder.DropIndex(
                name: "IX_Cures_DossierId",
                table: "Cures");

            migrationBuilder.DropColumn(
                name: "DossierId",
                table: "Cures");
        }
    }
}
