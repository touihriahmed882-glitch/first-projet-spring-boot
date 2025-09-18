using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace serviceoncologie.Migrations
{
    /// <inheritdoc />
    public partial class AddModificationAndSuppressionDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateModification",
                table: "ModuleUsers",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateSuppression",
                table: "ModuleUsers",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateModification",
                table: "ModuleUsers");

            migrationBuilder.DropColumn(
                name: "DateSuppression",
                table: "ModuleUsers");
        }
    }
}
