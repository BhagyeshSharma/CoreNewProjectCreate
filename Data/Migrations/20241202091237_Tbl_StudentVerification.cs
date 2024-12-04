using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Tbl_StudentVerification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_StudentVerifications",
                columns: table => new
                {
                    StdVerifcnID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StdVerifcnName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    StdVerifcnEmail = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    StdVerifcnMobileNo = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    IsVerified = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_StudentVerifications", x => x.StdVerifcnID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_StudentVerifications");
        }
    }
}
