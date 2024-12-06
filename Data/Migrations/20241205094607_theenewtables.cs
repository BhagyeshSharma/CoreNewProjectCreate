using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class theenewtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tbl_DepartmentDepartmentID",
                table: "Tbl_Team",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Tbl_TeamTeamId",
                table: "Tbl_Employee",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Team_Tbl_DepartmentDepartmentID",
                table: "Tbl_Team",
                column: "Tbl_DepartmentDepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Employee_Tbl_TeamTeamId",
                table: "Tbl_Employee",
                column: "Tbl_TeamTeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Employee_Tbl_Team_Tbl_TeamTeamId",
                table: "Tbl_Employee",
                column: "Tbl_TeamTeamId",
                principalTable: "Tbl_Team",
                principalColumn: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tbl_Team_Tbl_Department_Tbl_DepartmentDepartmentID",
                table: "Tbl_Team",
                column: "Tbl_DepartmentDepartmentID",
                principalTable: "Tbl_Department",
                principalColumn: "DepartmentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Employee_Tbl_Team_Tbl_TeamTeamId",
                table: "Tbl_Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Tbl_Team_Tbl_Department_Tbl_DepartmentDepartmentID",
                table: "Tbl_Team");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Team_Tbl_DepartmentDepartmentID",
                table: "Tbl_Team");

            migrationBuilder.DropIndex(
                name: "IX_Tbl_Employee_Tbl_TeamTeamId",
                table: "Tbl_Employee");

            migrationBuilder.DropColumn(
                name: "Tbl_DepartmentDepartmentID",
                table: "Tbl_Team");

            migrationBuilder.DropColumn(
                name: "Tbl_TeamTeamId",
                table: "Tbl_Employee");
        }
    }
}
