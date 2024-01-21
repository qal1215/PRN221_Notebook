using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageSchoolScore.Migrations
{
    /// <inheritdoc />
    public partial class addyear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchoolYear",
                table: "StudentCsvs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolYear",
                table: "StudentCsvs");
        }
    }
}
