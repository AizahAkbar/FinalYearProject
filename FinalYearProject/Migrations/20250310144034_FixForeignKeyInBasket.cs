using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalYearProject.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyInBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_User_BakeId",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "BakeId",
                table: "Basket",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_BakeId",
                table: "Basket",
                newName: "IX_Basket_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_User_UserId",
                table: "Basket",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basket_User_UserId",
                table: "Basket");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Basket",
                newName: "BakeId");

            migrationBuilder.RenameIndex(
                name: "IX_Basket_UserId",
                table: "Basket",
                newName: "IX_Basket_BakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Basket_User_BakeId",
                table: "Basket",
                column: "BakeId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
