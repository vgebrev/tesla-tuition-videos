using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTV.DatabaseDeploy.Migrations
{
    /// <inheritdoc />
    public partial class TTV_AddUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "user");

            //NOTE: This table is managed by the TTV.Web.Auth project, we just want user Ids here for referencial integrity

            //migrationBuilder.CreateTable(
            //    name: "AspNetUsers",
            //    schema: "user",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "UserLesson",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLesson", x => new { x.LessonId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserLesson_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserLesson_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "VideoType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Full Lesson");

            migrationBuilder.CreateIndex(
                name: "IX_UserLesson_UserId",
                table: "UserLesson",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLesson");

            //NOTE: This table is managed by the TTV.Web.Auth project, we just want user Ids here for referencial integrity

            //migrationBuilder.DropTable(
            //    name: "AspNetUsers",
            //    schema: "user");

            migrationBuilder.UpdateData(
                table: "VideoType",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "FullLesson");
        }
    }
}
