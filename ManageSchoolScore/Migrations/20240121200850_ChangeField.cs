using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageSchoolScore.Migrations
{
    /// <inheritdoc />
    public partial class ChangeField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SchoolYear",
                table: "StudentCsvs",
                newName: "YearNow");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "YearNow",
                table: "StudentCsvs",
                newName: "SchoolYear");
        }
    }
}
