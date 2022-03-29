using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcFinal.DataAccess.Migrations
{
    public partial class updatesetget : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOwner",
                table: "ShoppingListUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOwner",
                table: "ShoppingListUsers");
        }
    }
}
