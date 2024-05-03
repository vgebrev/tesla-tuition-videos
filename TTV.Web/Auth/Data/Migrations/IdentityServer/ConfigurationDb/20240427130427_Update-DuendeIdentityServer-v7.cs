using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTV.Web.Auth.Data.Migrations.IdentityServer.ConfigurationDb
{
    /// <inheritdoc />
    public partial class UpdateDuendeIdentityServerv7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PushedAuthorizationLifetime",
                schema: "auth_cfg",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RequirePushedAuthorization",
                schema: "auth_cfg",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PushedAuthorizationLifetime",
                schema: "auth_cfg",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RequirePushedAuthorization",
                schema: "auth_cfg",
                table: "Clients");
        }
    }
}
