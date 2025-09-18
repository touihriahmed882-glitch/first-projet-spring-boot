using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class deletedossier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Dossiers_DossierId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_DecisionStafs_Dossiers_DossierId",
                table: "DecisionStafs");

            migrationBuilder.DropTable(
                name: "Dossiers");

            migrationBuilder.DropIndex(
                name: "IX_DecisionStafs_DossierId",
                table: "DecisionStafs");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_DossierId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "DossierId",
                table: "DecisionStafs");

            migrationBuilder.DropColumn(
                name: "DossierId",
                table: "Consultations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DossierId",
                table: "DecisionStafs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DossierId",
                table: "Consultations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Dossiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateCreation = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NumeroDossier = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dossiers", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DecisionStafs_DossierId",
                table: "DecisionStafs",
                column: "DossierId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_DossierId",
                table: "Consultations",
                column: "DossierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Dossiers_DossierId",
                table: "Consultations",
                column: "DossierId",
                principalTable: "Dossiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DecisionStafs_Dossiers_DossierId",
                table: "DecisionStafs",
                column: "DossierId",
                principalTable: "Dossiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
