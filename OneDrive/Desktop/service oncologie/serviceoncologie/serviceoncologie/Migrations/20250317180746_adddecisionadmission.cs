using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class adddecisionadmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DecisionStafs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Observation = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConsultationId = table.Column<int>(type: "int", nullable: false),
                    CommissionStafId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DecisionStafs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DecisionStafs_CommissionStafs_CommissionStafId",
                        column: x => x.CommissionStafId,
                        principalTable: "CommissionStafs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DecisionStafs_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Admissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateAdmission = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MotifAdmission = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateSortie = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    MotifSortie = table.Column<string>(type: "text", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DecisionStafId = table.Column<int>(type: "int", nullable: false),
                    ConsultationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admissions_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Admissions_DecisionStafs_DecisionStafId",
                        column: x => x.DecisionStafId,
                        principalTable: "DecisionStafs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_ConsultationId",
                table: "Admissions",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_DecisionStafId",
                table: "Admissions",
                column: "DecisionStafId");

            migrationBuilder.CreateIndex(
                name: "IX_DecisionStafs_CommissionStafId",
                table: "DecisionStafs",
                column: "CommissionStafId");

            migrationBuilder.CreateIndex(
                name: "IX_DecisionStafs_ConsultationId",
                table: "DecisionStafs",
                column: "ConsultationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admissions");

            migrationBuilder.DropTable(
                name: "DecisionStafs");
        }
    }
}
