using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTV.DatabaseDeploy.Migrations
{
    /// <inheritdoc />
    public partial class AddCurriculaToLearningPaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearningPathItems_LearningPathItems_ParentId",
                table: "LearningPathItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LearningPathItems_LearningPath_LearningPathId",
                table: "LearningPathItems");

            migrationBuilder.DropForeignKey(
                name: "FK_LearningPathItems_Lesson_LessonId",
                table: "LearningPathItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LearningPathItems",
                table: "LearningPathItems");

            migrationBuilder.RenameTable(
                name: "LearningPathItems",
                newName: "LearningPathItem");

            migrationBuilder.RenameIndex(
                name: "IX_LearningPathItems_ParentId",
                table: "LearningPathItem",
                newName: "IX_LearningPathItem_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_LearningPathItems_LessonId",
                table: "LearningPathItem",
                newName: "IX_LearningPathItem_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_LearningPathItems_LearningPathId",
                table: "LearningPathItem",
                newName: "IX_LearningPathItem_LearningPathId");

            migrationBuilder.AddColumn<string>(
                name: "Curricula",
                table: "LearningPath",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Curricula",
                table: "LearningPathItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LearningPathItem",
                table: "LearningPathItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LearningPathItem_LearningPathItem_ParentId",
                table: "LearningPathItem",
                column: "ParentId",
                principalTable: "LearningPathItem",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LearningPathItem_LearningPath_LearningPathId",
                table: "LearningPathItem",
                column: "LearningPathId",
                principalTable: "LearningPath",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LearningPathItem_Lesson_LessonId",
                table: "LearningPathItem",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LearningPathItem_LearningPathItem_ParentId",
                table: "LearningPathItem");

            migrationBuilder.DropForeignKey(
                name: "FK_LearningPathItem_LearningPath_LearningPathId",
                table: "LearningPathItem");

            migrationBuilder.DropForeignKey(
                name: "FK_LearningPathItem_Lesson_LessonId",
                table: "LearningPathItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LearningPathItem",
                table: "LearningPathItem");

            migrationBuilder.DropColumn(
                name: "Curricula",
                table: "LearningPath");

            migrationBuilder.DropColumn(
                name: "Curricula",
                table: "LearningPathItem");

            migrationBuilder.RenameTable(
                name: "LearningPathItem",
                newName: "LearningPathItems");

            migrationBuilder.RenameIndex(
                name: "IX_LearningPathItem_ParentId",
                table: "LearningPathItems",
                newName: "IX_LearningPathItems_ParentId");

            migrationBuilder.RenameIndex(
                name: "IX_LearningPathItem_LessonId",
                table: "LearningPathItems",
                newName: "IX_LearningPathItems_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_LearningPathItem_LearningPathId",
                table: "LearningPathItems",
                newName: "IX_LearningPathItems_LearningPathId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LearningPathItems",
                table: "LearningPathItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LearningPathItems_LearningPathItems_ParentId",
                table: "LearningPathItems",
                column: "ParentId",
                principalTable: "LearningPathItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LearningPathItems_LearningPath_LearningPathId",
                table: "LearningPathItems",
                column: "LearningPathId",
                principalTable: "LearningPath",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LearningPathItems_Lesson_LessonId",
                table: "LearningPathItems",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id");
        }
    }
}
