using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class ajouterentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CommissionStaffs_CommissionStaffId",
                table: "Users");

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
                name: "IX_CommissionStafMedecins_CommissionStafId",
                table: "CommissionStafMedecins",
                column: "CommissionStafId");

            migrationBuilder.CreateIndex(
                name: "IX_CommissionStafMedecins_MedecinId",
                table: "CommissionStafMedecins",
                column: "MedecinId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CommissionStaffs_CommissionStaffId",
                table: "Users",
                column: "CommissionStaffId",
                principalTable: "CommissionStaffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CommissionStaffs_CommissionStaffId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "CommissionStafMedecins");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CommissionStaffs_CommissionStaffId",
                table: "Users",
                column: "CommissionStaffId",
                principalTable: "CommissionStaffs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
