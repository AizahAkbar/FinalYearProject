using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalYearProject.Migrations
{
    /// <inheritdoc />
    public partial class AmendBakesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Review",
                newName: "User");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Review",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Review");

            migrationBuilder.RenameColumn(
                name: "User",
                table: "Review",
                newName: "Name");
        }
    }
}
