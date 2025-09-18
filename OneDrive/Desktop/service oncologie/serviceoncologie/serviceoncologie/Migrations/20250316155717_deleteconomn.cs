using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class deleteconomn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CommissionStaffs_CommissionStaffId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CommissionStaffId",
                table: "Users",
                newName: "CommissionStafId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CommissionStaffId",
                table: "Users",
                newName: "IX_Users_CommissionStafId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CommissionStaffs_CommissionStafId",
                table: "Users",
                column: "CommissionStafId",
                principalTable: "CommissionStaffs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_CommissionStaffs_CommissionStafId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "CommissionStafId",
                table: "Users",
                newName: "CommissionStaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_CommissionStafId",
                table: "Users",
                newName: "IX_Users_CommissionStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_CommissionStaffs_CommissionStaffId",
                table: "Users",
                column: "CommissionStaffId",
                principalTable: "CommissionStaffs",
                principalColumn: "Id");
        }
    }
}
