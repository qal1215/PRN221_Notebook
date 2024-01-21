using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ManageSchoolScore.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SchoolYears",
                columns: new[] { "Id", "ExamYear", "Name", "Status" },
                values: new object[,]
                {
                    { 1L, 2017, "2017", "" },
                    { 2L, 2018, "2018", "" },
                    { 3L, 2019, "2019", "" },
                    { 4L, 2020, "2020", "" },
                    { 5L, 2021, "2021", "" },
                    { 6L, 2022, "2022", "" },
                    { 7L, 2023, "2023", "" },
                    { 8L, 2024, "2024", "" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1L, "MATH", "Toán" },
                    { 2L, "LIT", "Văn" },
                    { 3L, "PHYS", "Lý" },
                    { 4L, "CHEM", "Hoá" },
                    { 5L, "BIO", "Sinh" },
                    { 6L, "CNS", "Khoa học tự nhiên" },
                    { 7L, "HIST", "Sử" },
                    { 8L, "GEO", "Địa" },
                    { 9L, "CIV", "Giáo dục công dân" },
                    { 10L, "CSS", "Khoa học xã hội" },
                    { 11L, "ENG", "English" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SchoolYears",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "SchoolYears",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "SchoolYears",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "SchoolYears",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "SchoolYears",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "SchoolYears",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "SchoolYears",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "SchoolYears",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 11L);
        }
    }
}
