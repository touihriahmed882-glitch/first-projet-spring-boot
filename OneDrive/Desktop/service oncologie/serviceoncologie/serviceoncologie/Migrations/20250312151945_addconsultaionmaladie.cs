using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class addconsultaionmaladie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConsultationId",
                table: "Maladies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Consultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateConsultation = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Tension = table.Column<double>(type: "double", nullable: false),
                    Temperature = table.Column<double>(type: "double", nullable: false),
                    Poids = table.Column<double>(type: "double", nullable: false),
                    Taille = table.Column<double>(type: "double", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    MedecinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultations_Users_MedecinId",
                        column: x => x.MedecinId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ConsultationMaladies",
                columns: table => new
                {
                    ConsultationId = table.Column<int>(type: "int", nullable: false),
                    MaladieId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationMaladies", x => new { x.ConsultationId, x.MaladieId });
                    table.ForeignKey(
                        name: "FK_ConsultationMaladies_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsultationMaladies_Maladies_MaladieId",
                        column: x => x.MaladieId,
                        principalTable: "Maladies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Maladies_ConsultationId",
                table: "Maladies",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationMaladies_MaladieId",
                table: "ConsultationMaladies",
                column: "MaladieId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_MedecinId",
                table: "Consultations",
                column: "MedecinId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_PatientId",
                table: "Consultations",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maladies_Consultations_ConsultationId",
                table: "Maladies",
                column: "ConsultationId",
                principalTable: "Consultations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maladies_Consultations_ConsultationId",
                table: "Maladies");

            migrationBuilder.DropTable(
                name: "ConsultationMaladies");

            migrationBuilder.DropTable(
                name: "Consultations");

            migrationBuilder.DropIndex(
                name: "IX_Maladies_ConsultationId",
                table: "Maladies");

            migrationBuilder.DropColumn(
                name: "ConsultationId",
                table: "Maladies");
        }
    }
}
