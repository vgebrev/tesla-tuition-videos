using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTV.DatabaseDeploy.Migrations
{
    /// <inheritdoc />
    public partial class TTV_Notifications_Order_User_Relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Notification",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_OrderId",
                table: "Notification",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_UserId",
                table: "Notification",
                column: "UserId",
                principalSchema: "user",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Order_OrderId",
                table: "Notification",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_UserId",
                table: "Notification");

            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Order_OrderId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_OrderId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_UserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notification");
        }
    }
}
