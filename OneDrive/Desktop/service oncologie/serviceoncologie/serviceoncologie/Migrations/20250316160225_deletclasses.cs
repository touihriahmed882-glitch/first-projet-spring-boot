using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class deletclasses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CommissionStaffs_CommissionStafId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CommissionStafMedecins");

            migrationBuilder.DropTable(
                name: "CommissionStaffs");

            migrationBuilder.DropIndex(
                name: "IX_Users_CommissionStafId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CommissionStafId",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommissionStafId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CommissionStaffs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateCommission = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nom = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionStaffs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CommissionStafMedecins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CommissionStafId = table.Column<int>(type: "int", nullable: false),
                    MedecinId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommissionStafMedecins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommissionStafMedecins_CommissionStaffs_CommissionStafId",
                        column: x => x.CommissionStafId,
                        principalTable: "CommissionStaffs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommissionStafMedecins_Users_MedecinId",
                        column: x => x.MedecinId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CommissionStafId",
                table: "Users",
                column: "CommissionStafId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionStafMedecins_CommissionStafId",
                table: "CommissionStafMedecins",
                column: "CommissionStafId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionStafMedecins_MedecinId",
                table: "CommissionStafMedecins",
                column: "MedecinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CommissionStaffs_CommissionStafId",
                table: "Users",
                column: "CommissionStafId",
                principalTable: "CommissionStaffs",
                principalColumn: "Id");
        }
    }
}
