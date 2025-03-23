using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TTV.DatabaseDeploy.Migrations
{
    /// <inheritdoc />
    public partial class AddLearningPaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curriculum",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculum", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearningPath",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPath", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LearningPathItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    LearningPathId = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LearningPathItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LearningPathItems_LearningPathItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "LearningPathItems",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LearningPathItems_LearningPath_LearningPathId",
                        column: x => x.LearningPathId,
                        principalTable: "LearningPath",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LearningPathItems_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Curriculum",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "CAPS" },
                    { 2, "IEB" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LearningPathItems_LearningPathId",
                table: "LearningPathItems",
                column: "LearningPathId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPathItems_LessonId",
                table: "LearningPathItems",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_LearningPathItems_ParentId",
                table: "LearningPathItems",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Curriculum");

            migrationBuilder.DropTable(
                name: "LearningPathItems");

            migrationBuilder.DropTable(
                name: "LearningPath");
        }
    }
}
