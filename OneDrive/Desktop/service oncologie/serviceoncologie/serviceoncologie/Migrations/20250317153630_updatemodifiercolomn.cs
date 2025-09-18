using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class updatemodifiercolomn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StafMedecins_CommissionStafs_CommissionStafId",
                table: "StafMedecins");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "StafMedecins",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "CommissionStafId",
                table: "StafMedecins",
                newName: "commissionstafid");

            migrationBuilder.RenameIndex(
                name: "IX_StafMedecins_UserId",
                table: "StafMedecins",
                newName: "IX_StafMedecins_userid");

            migrationBuilder.RenameIndex(
                name: "IX_StafMedecins_CommissionStafId",
                table: "StafMedecins",
                newName: "IX_StafMedecins_commissionstafid");

            migrationBuilder.AddForeignKey(
                name: "FK_StafMedecins_CommissionStafs_commissionstafid",
                table: "StafMedecins",
                column: "commissionstafid",
                principalTable: "CommissionStafs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StafMedecins_CommissionStafs_commissionstafid",
                table: "StafMedecins");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "StafMedecins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "commissionstafid",
                table: "StafMedecins",
                newName: "CommissionStafId");

            migrationBuilder.RenameIndex(
                name: "IX_StafMedecins_userid",
                table: "StafMedecins",
                newName: "IX_StafMedecins_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StafMedecins_commissionstafid",
                table: "StafMedecins",
                newName: "IX_StafMedecins_CommissionStafId");

            migrationBuilder.AddForeignKey(
                name: "FK_StafMedecins_CommissionStafs_CommissionStafId",
                table: "StafMedecins",
                column: "CommissionStafId",
                principalTable: "CommissionStafs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
