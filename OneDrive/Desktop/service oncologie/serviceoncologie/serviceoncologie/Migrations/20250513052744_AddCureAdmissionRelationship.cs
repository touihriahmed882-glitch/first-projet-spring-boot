using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class AddCureAdmissionRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdmissionId",
                table: "Cures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cures_AdmissionId",
                table: "Cures",
                column: "AdmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cures_Admissions_AdmissionId",
                table: "Cures",
                column: "AdmissionId",
                principalTable: "Admissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cures_Admissions_AdmissionId",
                table: "Cures");

            migrationBuilder.DropIndex(
                name: "IX_Cures_AdmissionId",
                table: "Cures");

            migrationBuilder.DropColumn(
                name: "AdmissionId",
                table: "Cures");
        }
    }
}
