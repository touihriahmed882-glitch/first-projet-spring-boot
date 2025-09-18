using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class addrelationrdvpatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rdvs_Patients_PatientId",
                table: "Rdvs");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Rdvs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rdvs_Patients_PatientId",
                table: "Rdvs",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rdvs_Patients_PatientId",
                table: "Rdvs");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Rdvs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rdvs_Patients_PatientId",
                table: "Rdvs",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
