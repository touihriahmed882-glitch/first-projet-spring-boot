using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class AddProtocoleRelationToDecisionStaf : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProtocoleId",
                table: "DecisionStafs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProtocoleId1",
                table: "DecisionStafs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DecisionStafs_ProtocoleId",
                table: "DecisionStafs",
                column: "ProtocoleId");

            migrationBuilder.CreateIndex(
                name: "IX_DecisionStafs_ProtocoleId1",
                table: "DecisionStafs",
                column: "ProtocoleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DecisionStafs_Protocoles_ProtocoleId",
                table: "DecisionStafs",
                column: "ProtocoleId",
                principalTable: "Protocoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DecisionStafs_Protocoles_ProtocoleId1",
                table: "DecisionStafs",
                column: "ProtocoleId1",
                principalTable: "Protocoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DecisionStafs_Protocoles_ProtocoleId",
                table: "DecisionStafs");

            migrationBuilder.DropForeignKey(
                name: "FK_DecisionStafs_Protocoles_ProtocoleId1",
                table: "DecisionStafs");

            migrationBuilder.DropIndex(
                name: "IX_DecisionStafs_ProtocoleId",
                table: "DecisionStafs");

            migrationBuilder.DropIndex(
                name: "IX_DecisionStafs_ProtocoleId1",
                table: "DecisionStafs");

            migrationBuilder.DropColumn(
                name: "ProtocoleId",
                table: "DecisionStafs");

            migrationBuilder.DropColumn(
                name: "ProtocoleId1",
                table: "DecisionStafs");
        }
    }
}
