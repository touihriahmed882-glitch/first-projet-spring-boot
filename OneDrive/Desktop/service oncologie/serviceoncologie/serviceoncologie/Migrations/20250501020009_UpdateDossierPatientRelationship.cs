using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDossierPatientRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_DossierId",
                table: "Patients");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DossierId",
                table: "Patients",
                column: "DossierId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_DossierId",
                table: "Patients");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DossierId",
                table: "Patients",
                column: "DossierId");
        }
    }
}
