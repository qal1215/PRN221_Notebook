using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageSchoolScore.Migrations
{
    /// <inheritdoc />
    public partial class addscvtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentCsvs",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mathematics = table.Column<double>(type: "float", nullable: true),
                    Literature = table.Column<double>(type: "float", nullable: true),
                    Physics = table.Column<double>(type: "float", nullable: true),
                    Chemistry = table.Column<double>(type: "float", nullable: true),
                    Biology = table.Column<double>(type: "float", nullable: true),
                    CombinedNaturalSciences = table.Column<double>(type: "float", nullable: true),
                    History = table.Column<double>(type: "float", nullable: true),
                    Geography = table.Column<double>(type: "float", nullable: true),
                    CivicEducation = table.Column<double>(type: "float", nullable: true),
                    CombinedSocialSciences = table.Column<double>(type: "float", nullable: true),
                    English = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCsvs", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentCsvs");
        }
    }
}
