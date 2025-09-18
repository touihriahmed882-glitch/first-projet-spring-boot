using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class deleteclasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DemandesRdvs_Rdvs_RdvId",
                table: "DemandesRdvs");

            migrationBuilder.DropTable(
                name: "Paiements");

            migrationBuilder.DropTable(
                name: "Rdvs");

            migrationBuilder.DropTable(
                name: "DemandesRdvs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DemandesRdvs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    MedecinId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    RdvId = table.Column<int>(type: "int", nullable: true),
                    DateSouhaitee = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Statut = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandesRdvs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandesRdvs_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DemandesRdvs_Users_MedecinId",
                        column: x => x.MedecinId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rdvs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DemandeRdvId = table.Column<int>(type: "int", nullable: true),
                    MedecinId = table.Column<int>(type: "int", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DateRdv = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rdvs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rdvs_DemandesRdvs_DemandeRdvId",
                        column: x => x.DemandeRdvId,
                        principalTable: "DemandesRdvs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Rdvs_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rdvs_Users_MedecinId",
                        column: x => x.MedecinId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rdvs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Paiements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    RdvId = table.Column<int>(type: "int", nullable: false),
                    DatePaiement = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModePaiement = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Montant = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Statut = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paiements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paiements_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Paiements_Rdvs_RdvId",
                        column: x => x.RdvId,
                        principalTable: "Rdvs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DemandesRdvs_MedecinId",
                table: "DemandesRdvs",
                column: "MedecinId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandesRdvs_PatientId",
                table: "DemandesRdvs",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_DemandesRdvs_RdvId",
                table: "DemandesRdvs",
                column: "RdvId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_PatientId",
                table: "Paiements",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Paiements_RdvId",
                table: "Paiements",
                column: "RdvId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rdvs_DemandeRdvId",
                table: "Rdvs",
                column: "DemandeRdvId");

            migrationBuilder.CreateIndex(
                name: "IX_Rdvs_MedecinId",
                table: "Rdvs",
                column: "MedecinId");

            migrationBuilder.CreateIndex(
                name: "IX_Rdvs_PatientId",
                table: "Rdvs",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Rdvs_UserId",
                table: "Rdvs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DemandesRdvs_Rdvs_RdvId",
                table: "DemandesRdvs",
                column: "RdvId",
                principalTable: "Rdvs",
                principalColumn: "Id");
        }
    }
}
