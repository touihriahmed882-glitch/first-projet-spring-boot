using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class addrdv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rdvs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateRdv = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Etat = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MedecinId = table.Column<int>(type: "int", nullable: false),
                    DemandeRdvId = table.Column<int>(type: "int", nullable: false),
                    DemandeRdvId1 = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rdvs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rdvs_DemandesRdv_DemandeRdvId",
                        column: x => x.DemandeRdvId,
                        principalTable: "DemandesRdv",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rdvs_DemandesRdv_DemandeRdvId1",
                        column: x => x.DemandeRdvId1,
                        principalTable: "DemandesRdv",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rdvs_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rdvs_Users_MedecinId",
                        column: x => x.MedecinId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Rdvs_DemandeRdvId",
                table: "Rdvs",
                column: "DemandeRdvId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rdvs_DemandeRdvId1",
                table: "Rdvs",
                column: "DemandeRdvId1",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rdvs_MedecinId",
                table: "Rdvs",
                column: "MedecinId");

            migrationBuilder.CreateIndex(
                name: "IX_Rdvs_PatientId",
                table: "Rdvs",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rdvs");
        }
    }
}
