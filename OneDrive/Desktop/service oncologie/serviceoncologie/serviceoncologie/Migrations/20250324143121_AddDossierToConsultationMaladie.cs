using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class AddDossierToConsultationMaladie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DossierId",
                table: "ConsultationMaladies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationMaladies_DossierId",
                table: "ConsultationMaladies",
                column: "DossierId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultationMaladies_Dossiers_DossierId",
                table: "ConsultationMaladies",
                column: "DossierId",
                principalTable: "Dossiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultationMaladies_Dossiers_DossierId",
                table: "ConsultationMaladies");

            migrationBuilder.DropIndex(
                name: "IX_ConsultationMaladies_DossierId",
                table: "ConsultationMaladies");

            migrationBuilder.DropColumn(
                name: "DossierId",
                table: "ConsultationMaladies");
        }
    }
}
