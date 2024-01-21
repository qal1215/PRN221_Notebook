using System.Collections.Generic;

namespace ManageSchoolScore.Models
{
    public class Subject : BaseModel
    {
        public string Code { get; set; } = "";

        public string Name { get; set; } = "";
    }

    public static class SubjectSeed
    {
        public static readonly List<Subject> Subjects = new List<Subject>
        {
            new Subject { Id = 1, Code = "MATH", Name = "Toán" },
            new Subject { Id = 2, Code = "LIT", Name = "Văn" },
            new Subject { Id = 3, Code = "PHYS", Name = "Lý" },
            new Subject { Id = 4, Code = "CHEM", Name = "Hoá" },
            new Subject { Id = 5, Code = "BIO", Name = "Sinh" },
            new Subject { Id = 6, Code = "CNS", Name = "Khoa học tự nhiên" },
            new Subject { Id = 7, Code = "HIST", Name = "Sử" },
            new Subject { Id = 8, Code = "GEO", Name = "Địa" },
            new Subject { Id = 9, Code = "CIV", Name = "Giáo dục công dân" },
            new Subject { Id = 10, Code = "CSS", Name = "Khoa học xã hội" },
            new Subject { Id = 11, Code = "ENG", Name = "English" }
        };
    }
}
