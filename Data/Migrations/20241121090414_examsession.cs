using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class examsession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblExamSessions",
                columns: table => new
                {
                    ExamSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblExamSessions", x => x.ExamSessionId);
                });

            migrationBuilder.CreateTable(
                name: "TblQuestion",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionText = table.Column<string>(type: "nvarchar(max)", maxLength: 500, nullable: false),
                    OptionA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OptionB = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OptionC = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    OptionD = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CorrectOption = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DifficultyLevel = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblQuestion", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "TblUserActivityLogs",
                columns: table => new
                {
                    ActivitylogId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    EventType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserActivityLogs", x => x.ActivitylogId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblExamSessions");

            migrationBuilder.DropTable(
                name: "TblQuestion");

            migrationBuilder.DropTable(
                name: "TblUserActivityLogs");
        }
    }
}
