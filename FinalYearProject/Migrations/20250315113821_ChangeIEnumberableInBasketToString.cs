using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalYearProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIEnumberableInBasketToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bake_Basket_BasketId",
                table: "Bake");

            migrationBuilder.DropIndex(
                name: "IX_Bake_BasketId",
                table: "Bake");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "Bake");

            migrationBuilder.AddColumn<string>(
                name: "Bakes",
                table: "Basket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bakes",
                table: "Basket");

            migrationBuilder.AddColumn<int>(
                name: "BasketId",
                table: "Bake",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bake_BasketId",
                table: "Bake",
                column: "BasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bake_Basket_BasketId",
                table: "Bake",
                column: "BasketId",
                principalTable: "Basket",
                principalColumn: "Id");
        }
    }
}
