using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageSchoolScore.Migrations
{
    /// <inheritdoc />
    public partial class addTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopA00",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "TopA00_Id",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "TopA01",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "TopA01_Id",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "TopB00",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "TopB00_Id",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "TopC00",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "TopC00_Id",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "TopD01",
                table: "Statistics");

            migrationBuilder.DropColumn(
                name: "TopD01_Id",
                table: "Statistics");

            migrationBuilder.CreateTable(
                name: "TopScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolYear = table.Column<int>(type: "int", nullable: false),
                    KhoiThi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopScores", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopScores");

            migrationBuilder.AddColumn<double>(
                name: "TopA00",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TopA00_Id",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TopA01",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TopA01_Id",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TopB00",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TopB00_Id",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TopC00",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TopC00_Id",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "TopD01",
                table: "Statistics",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "TopD01_Id",
                table: "Statistics",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
