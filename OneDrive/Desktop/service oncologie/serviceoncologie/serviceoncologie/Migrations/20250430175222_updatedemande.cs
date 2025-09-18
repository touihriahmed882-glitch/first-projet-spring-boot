using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class updatedemande : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rdvs_DemandesRdv_DemandeRdvId",
                table: "Rdvs");

            migrationBuilder.DropForeignKey(
                name: "FK_Rdvs_DemandesRdv_DemandeRdvId1",
                table: "Rdvs");

            migrationBuilder.DropTable(
                name: "DemandesRdv");

            migrationBuilder.DropIndex(
                name: "IX_Rdvs_DemandeRdvId",
                table: "Rdvs");

            migrationBuilder.DropIndex(
                name: "IX_Rdvs_DemandeRdvId1",
                table: "Rdvs");

            migrationBuilder.DropColumn(
                name: "DemandeRdvId",
                table: "Rdvs");

            migrationBuilder.DropColumn(
                name: "DemandeRdvId1",
                table: "Rdvs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DemandeRdvId",
                table: "Rdvs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DemandeRdvId1",
                table: "Rdvs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DemandesRdv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DateDeDemande = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DemandesRdv", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DemandesRdv_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_DemandesRdv_PatientId",
                table: "DemandesRdv",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rdvs_DemandesRdv_DemandeRdvId",
                table: "Rdvs",
                column: "DemandeRdvId",
                principalTable: "DemandesRdv",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rdvs_DemandesRdv_DemandeRdvId1",
                table: "Rdvs",
                column: "DemandeRdvId1",
                principalTable: "DemandesRdv",
                principalColumn: "Id");
        }
    }
}
