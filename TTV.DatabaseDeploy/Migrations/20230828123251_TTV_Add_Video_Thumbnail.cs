using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TTV.DatabaseDeploy.Migrations
{
    /// <inheritdoc />
    public partial class TTV_Add_Video_Thumbnail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_Lesson_VideoId",
                table: "Video");

            migrationBuilder.RenameColumn(
                name: "VideoId",
                table: "Video",
                newName: "LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_Video_VideoId",
                table: "Video",
                newName: "IX_Video_LessonId");

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "Video",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Awaiting Payment");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Lesson_LessonId",
                table: "Video",
                column: "LessonId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Video_Lesson_LessonId",
                table: "Video");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "Video");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Video",
                newName: "VideoId");

            migrationBuilder.RenameIndex(
                name: "IX_Video_LessonId",
                table: "Video",
                newName: "IX_Video_VideoId");

            migrationBuilder.UpdateData(
                table: "OrderStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Processing");

            migrationBuilder.AddForeignKey(
                name: "FK_Video_Lesson_VideoId",
                table: "Video",
                column: "VideoId",
                principalTable: "Lesson",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
