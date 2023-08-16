using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace TTV.DatabaseDeploy.Migrations
{
    /// <inheritdoc />
    public partial class TTV_Create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LessonType",
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
                name: "TagCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_TagCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TagCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoTypeId = table.Column<int>(type: "int", nullable: false),
                    Filename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelativePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntroVideoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Video_VideoType_VideoTypeId",
                        column: x => x.VideoTypeId,
                        principalTable: "VideoType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Video_Video_IntroVideoId",
                        column: x => x.IntroVideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonTypeId = table.Column<int>(type: "int", nullable: false),
                    VideoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lesson_LessonType_LessonTypeId",
                        column: x => x.LessonTypeId,
                        principalTable: "LessonType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Lesson_Video_VideoId",
                        column: x => x.VideoId,
                        principalTable: "Video",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonTag",
                columns: table => new
                {
                    LessonId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonTag", x => new { x.LessonId, x.TagId });
                    table.ForeignKey(
                        name: "FK_LessonTag_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "LessonType",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Video" });

            migrationBuilder.InsertData(
                table: "VideoType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Full Lesson" },
                    { 2, "Intro" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_LessonTypeId",
                table: "Lesson",
                column: "LessonTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_VideoId",
                table: "Lesson",
                column: "VideoId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonTag_TagId",
                table: "LessonTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_CategoryId",
                table: "Tag",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_IntroVideoId",
                table: "Video",
                column: "IntroVideoId",
                unique: true,
                filter: "[IntroVideoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Video_VideoTypeId",
                table: "Video",
                column: "VideoTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LessonTag");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "LessonType");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.DropTable(
                name: "TagCategory");

            migrationBuilder.DropTable(
                name: "VideoType");
        }
    }
}
