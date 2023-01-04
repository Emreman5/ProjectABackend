using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class init6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Menus_CategoryId",
                table: "Menus",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Categories_CategoryId",
                table: "Menus",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Categories_CategoryId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_CategoryId",
                table: "Menus");
        }
    }
}
