using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class addjadwaldecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DecisionStafId",
                table: "Cures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cures_DecisionStafId",
                table: "Cures",
                column: "DecisionStafId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cures_DecisionStafs_DecisionStafId",
                table: "Cures",
                column: "DecisionStafId",
                principalTable: "DecisionStafs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cures_DecisionStafs_DecisionStafId",
                table: "Cures");

            migrationBuilder.DropIndex(
                name: "IX_Cures_DecisionStafId",
                table: "Cures");

            migrationBuilder.DropColumn(
                name: "DecisionStafId",
                table: "Cures");
        }
    }
}
