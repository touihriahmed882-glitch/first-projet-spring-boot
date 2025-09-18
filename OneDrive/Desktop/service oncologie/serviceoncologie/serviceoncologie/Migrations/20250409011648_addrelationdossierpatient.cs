using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class addrelationdossierpatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DossierId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DossierId",
                table: "Patients",
                column: "DossierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Dossiers_DossierId",
                table: "Patients",
                column: "DossierId",
                principalTable: "Dossiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Dossiers_DossierId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DossierId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DossierId",
                table: "Patients");
        }
    }
}
