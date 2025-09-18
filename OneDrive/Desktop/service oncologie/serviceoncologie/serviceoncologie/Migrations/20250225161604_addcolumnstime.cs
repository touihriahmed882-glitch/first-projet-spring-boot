using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class addcolumnstime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "TacheUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "TacheUsers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "TacheUsers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "TacheUsers",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "TacheUsers");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "TacheUsers");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "TacheUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TacheUsers",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
