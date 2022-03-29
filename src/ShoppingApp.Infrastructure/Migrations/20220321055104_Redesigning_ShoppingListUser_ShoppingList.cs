using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFinal.DataAccess.Migrations
{
    public partial class Redesigning_ShoppingListUser_ShoppingList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingLists_Users_OwnerId",
                table: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingLists_OwnerId",
                table: "ShoppingLists");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ShoppingLists");

            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "ShoppingListAccessibleUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "ShoppingListAccessibleUsers");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "ShoppingLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingLists_OwnerId",
                table: "ShoppingLists",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingLists_Users_OwnerId",
                table: "ShoppingLists",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
