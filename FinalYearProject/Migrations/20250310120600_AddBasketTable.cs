using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalYearProject.Migrations
{
    /// <inheritdoc />
    public partial class AddBasketTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasketId",
                table: "Bake",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basket", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bake_Basket_BasketId",
                table: "Bake");

            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropIndex(
                name: "IX_Bake_BasketId",
                table: "Bake");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "Bake");
        }
    }
}
