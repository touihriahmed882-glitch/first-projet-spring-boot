using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class eviterrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DecisionStafs_Protocoles_ProtocoleId1",
                table: "DecisionStafs");

            migrationBuilder.DropForeignKey(
                name: "FK_Protocoles_DecisionStafs_DecisionStafId",
                table: "Protocoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Protocoles_Dossiers_DossierId",
                table: "Protocoles");

            migrationBuilder.DropIndex(
                name: "IX_Protocoles_DecisionStafId",
                table: "Protocoles");

            migrationBuilder.DropIndex(
                name: "IX_Protocoles_DossierId",
                table: "Protocoles");

            migrationBuilder.DropIndex(
                name: "IX_DecisionStafs_ProtocoleId1",
                table: "DecisionStafs");

            migrationBuilder.DropColumn(
                name: "DecisionStafId",
                table: "Protocoles");

            migrationBuilder.DropColumn(
                name: "DossierId",
                table: "Protocoles");

            migrationBuilder.DropColumn(
                name: "ProtocoleId1",
                table: "DecisionStafs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DecisionStafId",
                table: "Protocoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DossierId",
                table: "Protocoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProtocoleId1",
                table: "DecisionStafs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Protocoles_DecisionStafId",
                table: "Protocoles",
                column: "DecisionStafId");

            migrationBuilder.CreateIndex(
                name: "IX_Protocoles_DossierId",
                table: "Protocoles",
                column: "DossierId");

            migrationBuilder.CreateIndex(
                name: "IX_DecisionStafs_ProtocoleId1",
                table: "DecisionStafs",
                column: "ProtocoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DecisionStafs_Protocoles_ProtocoleId1",
                table: "DecisionStafs",
                column: "ProtocoleId1",
                principalTable: "Protocoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Protocoles_DecisionStafs_DecisionStafId",
                table: "Protocoles",
                column: "DecisionStafId",
                principalTable: "DecisionStafs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Protocoles_Dossiers_DossierId",
                table: "Protocoles",
                column: "DossierId",
                principalTable: "Dossiers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
