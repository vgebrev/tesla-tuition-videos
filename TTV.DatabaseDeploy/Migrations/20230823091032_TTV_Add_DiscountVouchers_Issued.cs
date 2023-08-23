using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTV.DatabaseDeploy.Migrations
{
    /// <inheritdoc />
    public partial class TTV_Add_DiscountVouchers_Issued : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "IssuedAt",
                table: "DiscountVoucher",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "IssuedById",
                table: "DiscountVoucher",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "DiscountVoucher",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountVoucher_IssuedById",
                table: "DiscountVoucher",
                column: "IssuedById");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountVoucher_AspNetUsers_IssuedById",
                table: "DiscountVoucher",
                column: "IssuedById",
                principalSchema: "user",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountVoucher_AspNetUsers_IssuedById",
                table: "DiscountVoucher");

            migrationBuilder.DropIndex(
                name: "IX_DiscountVoucher_IssuedById",
                table: "DiscountVoucher");

            migrationBuilder.DropColumn(
                name: "IssuedAt",
                table: "DiscountVoucher");

            migrationBuilder.DropColumn(
                name: "IssuedById",
                table: "DiscountVoucher");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "DiscountVoucher");
        }
    }
}
