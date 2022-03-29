using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFinal.DataAccess.Migrations
{
    public partial class updatename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListAccessibleUsers_ShoppingLists_ShoppingListId",
                table: "ShoppingListAccessibleUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListAccessibleUsers_Users_UserId",
                table: "ShoppingListAccessibleUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingListAccessibleUsers",
                table: "ShoppingListAccessibleUsers");

            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "ShoppingListAccessibleUsers");

            migrationBuilder.RenameTable(
                name: "ShoppingListAccessibleUsers",
                newName: "ShoppingListUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListAccessibleUsers_UserId",
                table: "ShoppingListUsers",
                newName: "IX_ShoppingListUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListAccessibleUsers_ShoppingListId",
                table: "ShoppingListUsers",
                newName: "IX_ShoppingListUsers_ShoppingListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingListUsers",
                table: "ShoppingListUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListUsers_ShoppingLists_ShoppingListId",
                table: "ShoppingListUsers",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListUsers_Users_UserId",
                table: "ShoppingListUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListUsers_ShoppingLists_ShoppingListId",
                table: "ShoppingListUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingListUsers_Users_UserId",
                table: "ShoppingListUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingListUsers",
                table: "ShoppingListUsers");

            migrationBuilder.RenameTable(
                name: "ShoppingListUsers",
                newName: "ShoppingListAccessibleUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListUsers_UserId",
                table: "ShoppingListAccessibleUsers",
                newName: "IX_ShoppingListAccessibleUsers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingListUsers_ShoppingListId",
                table: "ShoppingListAccessibleUsers",
                newName: "IX_ShoppingListAccessibleUsers_ShoppingListId");

            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "ShoppingListAccessibleUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingListAccessibleUsers",
                table: "ShoppingListAccessibleUsers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListAccessibleUsers_ShoppingLists_ShoppingListId",
                table: "ShoppingListAccessibleUsers",
                column: "ShoppingListId",
                principalTable: "ShoppingLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingListAccessibleUsers_Users_UserId",
                table: "ShoppingListAccessibleUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
