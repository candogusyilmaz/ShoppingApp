using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFinal.DataAccess.Migrations
{
    public partial class FixShoppingList_Product_To_ShoppingListProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ShoppingLists_ShoppingListId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShoppingListId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShoppingListId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoppingListId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShoppingListId",
                table: "Products",
                column: "ShoppingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ShoppingLists_ShoppingListId",
                table: "Products",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id");
        }
    }
}
