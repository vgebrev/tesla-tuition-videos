using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTV.DatabaseDeploy.Migrations
{
    /// <inheritdoc />
    public partial class TTV_InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ttv");

            migrationBuilder.CreateTable(
                name: "LessonType",
                schema: "ttv",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                schema: "ttv",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_Lesson_LessonType_LessonTypeId",
                        column: x => x.LessonTypeId,
                        principalSchema: "ttv",
                        principalTable: "LessonType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "ttv",
                table: "LessonType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Video" });

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_LessonTypeId",
                schema: "ttv",
                table: "Lesson",
                column: "LessonTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lesson",
                schema: "ttv");

            migrationBuilder.DropTable(
                name: "LessonType",
                schema: "ttv");
        }
    }
}
