using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_API_Angular_Project.Migrations
{
    /// <inheritdoc />
    public partial class imageAsString2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_imageAsStrings_AspNetUsers_UserId",
                table: "imageAsStrings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "imageAsStrings",
                newName: "productId");

            migrationBuilder.RenameIndex(
                name: "IX_imageAsStrings_UserId",
                table: "imageAsStrings",
                newName: "IX_imageAsStrings_productId");

            migrationBuilder.AddForeignKey(
                name: "FK_imageAsStrings_Products_productId",
                table: "imageAsStrings",
                column: "productId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_imageAsStrings_Products_productId",
                table: "imageAsStrings");

            migrationBuilder.RenameColumn(
                name: "productId",
                table: "imageAsStrings",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_imageAsStrings_productId",
                table: "imageAsStrings",
                newName: "IX_imageAsStrings_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_imageAsStrings_AspNetUsers_UserId",
                table: "imageAsStrings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
