using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class addrelationadmissionpatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DossierId",
                table: "Admissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_DossierId",
                table: "Admissions",
                column: "DossierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_Dossiers_DossierId",
                table: "Admissions",
                column: "DossierId",
                principalTable: "Dossiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_Dossiers_DossierId",
                table: "Admissions");

            migrationBuilder.DropIndex(
                name: "IX_Admissions_DossierId",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "DossierId",
                table: "Admissions");
        }
    }
}
