using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class BlogInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblBlogPost",
                columns: table => new
                {
                    BlogPostId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBlogPost", x => x.BlogPostId);
                });

            migrationBuilder.CreateTable(
                name: "TblBlogComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TblBlogPostBlogPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBlogComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblBlogComments_TblBlogPost_TblBlogPostBlogPostId",
                        column: x => x.TblBlogPostBlogPostId,
                        principalTable: "TblBlogPost",
                        principalColumn: "BlogPostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblBlogLikes",
                columns: table => new
                {
                    LikeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogPostId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TblBlogPostBlogPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblBlogLikes", x => x.LikeId);
                    table.ForeignKey(
                        name: "FK_TblBlogLikes_TblBlogPost_TblBlogPostBlogPostId",
                        column: x => x.TblBlogPostBlogPostId,
                        principalTable: "TblBlogPost",
                        principalColumn: "BlogPostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblBlogComments_TblBlogPostBlogPostId",
                table: "TblBlogComments",
                column: "TblBlogPostBlogPostId");

            migrationBuilder.CreateIndex(
                name: "IX_TblBlogLikes_TblBlogPostBlogPostId",
                table: "TblBlogLikes",
                column: "TblBlogPostBlogPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblBlogComments");

            migrationBuilder.DropTable(
                name: "TblBlogLikes");

            migrationBuilder.DropTable(
                name: "TblBlogPost");
        }
    }
}
