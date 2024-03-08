using System.Collections.Generic;

namespace ManageSchoolScore.Models
{
    public class SchoolYear : BaseModel
    {
        public string Name { get; set; } = "";

        public int ExamYear { get; set; }

        public string Status { get; set; } = "";

    }

    public static class SchoolYearSeed
    {
        public static readonly List<SchoolYear> SchoolYears = new List<SchoolYear>();

        static SchoolYearSeed()
        {
            for (int year = 2017, id = 1; year <= 2024; year++, id++)
            {
                SchoolYears.Add(new SchoolYear
                {
                    Id = id,
                    ExamYear = year,
                    Name = year.ToString(),
                    Status = "" // Assuming Status is an empty string for now
                });
            }
        }
    }
}
