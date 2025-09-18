using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class deletpatientid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Paiements_Patients_PatientId",
                table: "Paiements");

            migrationBuilder.DropIndex(
                name: "IX_Paiements_PatientId",
                table: "Paiements");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Paiements");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Paiements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_PatientId",
                table: "Paiements",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Paiements_Patients_PatientId",
                table: "Paiements",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
