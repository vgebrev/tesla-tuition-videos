//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace TTV.DatabaseDeploy.Migrations
//{
//    /// <inheritdoc />
//    public partial class UpdatetoEFCore9 : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.AddColumn<string>(
//                name: "UserName",
//                schema: "user",
//                table: "AspNetUsers",
//                type: "nvarchar(max)",
//                nullable: false,
//                defaultValue: "");

//            migrationBuilder.CreateTable(
//                name: "AspNetUserClaims",
//                schema: "user",
//                columns: table => new
//                {
//                    Id = table.Column<int>(type: "int", nullable: false)
//                        .Annotation("SqlServer:Identity", "1, 1"),
//                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
//                        column: x => x.UserId,
//                        principalSchema: "user",
//                        principalTable: "AspNetUsers",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "AspNetUserLogins",
//                schema: "user",
//                columns: table => new
//                {
//                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
//                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
//                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey, x.UserId });
//                    table.ForeignKey(
//                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
//                        column: x => x.UserId,
//                        principalSchema: "user",
//                        principalTable: "AspNetUsers",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_AspNetUserClaims_UserId",
//                schema: "user",
//                table: "AspNetUserClaims",
//                column: "UserId");

//            migrationBuilder.CreateIndex(
//                name: "IX_AspNetUserLogins_UserId",
//                schema: "user",
//                table: "AspNetUserLogins",
//                column: "UserId");
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "AspNetUserClaims",
//                schema: "user");

//            migrationBuilder.DropTable(
//                name: "AspNetUserLogins",
//                schema: "user");

//            migrationBuilder.DropColumn(
//                name: "UserName",
//                schema: "user",
//                table: "AspNetUsers");
//        }
//    }
//}
