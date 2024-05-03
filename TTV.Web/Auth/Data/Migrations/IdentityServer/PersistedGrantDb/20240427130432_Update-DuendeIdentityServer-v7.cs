using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTV.Web.Auth.Data.Migrations.IdentityServer.PersistedGrantDb
{
    /// <inheritdoc />
    public partial class UpdateDuendeIdentityServerv7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey("PK_ServerSideSessions", "ServerSideSessions", "auth_ops");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                schema: "auth_ops",
                table: "ServerSideSessions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey("PK_ServerSideSessions", "ServerSideSessions", "Id", "auth_ops");

            migrationBuilder.CreateTable(
                name: "PushedAuthorizationRequests",
                schema: "auth_ops",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReferenceValueHash = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Parameters = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PushedAuthorizationRequests", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PushedAuthorizationRequests_ExpiresAtUtc",
                schema: "auth_ops",
                table: "PushedAuthorizationRequests",
                column: "ExpiresAtUtc");

            migrationBuilder.CreateIndex(
                name: "IX_PushedAuthorizationRequests_ReferenceValueHash",
                schema: "auth_ops",
                table: "PushedAuthorizationRequests",
                column: "ReferenceValueHash",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PushedAuthorizationRequests",
                schema: "auth_ops");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "auth_ops",
                table: "ServerSideSessions",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");
        }
    }
}
