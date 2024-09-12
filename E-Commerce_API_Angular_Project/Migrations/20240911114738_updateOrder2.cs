using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_API_Angular_Project.Migrations
{
    /// <inheritdoc />
    public partial class updateOrder2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Carts_cartId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_cartId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "cartId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "paymentId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_paymentId",
                table: "Orders",
                column: "paymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Payments_paymentId",
                table: "Orders",
                column: "paymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Payments_paymentId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_paymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "paymentId",
                table: "Orders");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "cartId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_cartId",
                table: "Orders",
                column: "cartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Carts_cartId",
                table: "Orders",
                column: "cartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
