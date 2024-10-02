using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_API_Angular_Project.Migrations
{
    /// <inheritdoc />
    public partial class imgTest3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserOtp_AspNetUsers_userID",
                table: "UserOtp");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserOtp",
                table: "UserOtp");

            migrationBuilder.RenameTable(
                name: "UserOtp",
                newName: "userOtps");

            migrationBuilder.RenameIndex(
                name: "IX_UserOtp_userID",
                table: "userOtps",
                newName: "IX_userOtps_userID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_userOtps",
                table: "userOtps",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ImgTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImgTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImgTest_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImgTest_UserId",
                table: "ImgTest",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_userOtps_AspNetUsers_userID",
                table: "userOtps",
                column: "userID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userOtps_AspNetUsers_userID",
                table: "userOtps");

            migrationBuilder.DropTable(
                name: "ImgTest");

            migrationBuilder.DropPrimaryKey(
                name: "PK_userOtps",
                table: "userOtps");

            migrationBuilder.RenameTable(
                name: "userOtps",
                newName: "UserOtp");

            migrationBuilder.RenameIndex(
                name: "IX_userOtps_userID",
                table: "UserOtp",
                newName: "IX_UserOtp_userID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserOtp",
                table: "UserOtp",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserOtp_AspNetUsers_userID",
                table: "UserOtp",
                column: "userID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
