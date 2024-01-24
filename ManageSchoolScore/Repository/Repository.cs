using ManageSchoolScore.DatabaseContextMSS;
using ManageSchoolScore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ManageSchoolScore.Repository
{
    public static class Repository
    {
        private static int YearNow = 0;

        private static uint SchoolYearId = 0;

        private static Hashtable? HashSubject = null;

        private static string[] SubjectCodes = new string[] {
                        "MATH",
                        "LIT",
                        "PHYS",
                        "CHEM",
                        "BIO",
                        "CNS",
                        "HIST",
                        "GEO",
                        "CIV",
                        "CSS",
                        "ENG"
                    };

        private static Hashtable? HashYear = null;


        public static StudentRaw ParseLineToStudent(string line)
        {
            var parts = line.Split(',');

            var student = new StudentRaw
            {
                ID = parts[0],
                Mathematics = TryParseNullableDouble(parts[1]),
                Literature = TryParseNullableDouble(parts[2]),
                Physics = TryParseNullableDouble(parts[3]),
                Biology = TryParseNullableDouble(parts[4]),
                English = TryParseNullableDouble(parts[5]),
                YearNow = TryParseNullableInt(parts[6]),
                Chemistry = TryParseNullableDouble(parts[7]),
                History = TryParseNullableDouble(parts[8]),
                Geography = TryParseNullableDouble(parts[9]),
                CivicEducation = TryParseNullableDouble(parts[10]),
                Province = parts[11],
            };

            student.ScoreTable = new Hashtable{
                {SubjectCodes[0], student.Mathematics},
                {SubjectCodes[1], student.Literature},
                {SubjectCodes[2], student.Physics},
                {SubjectCodes[3], student.Chemistry},
                {SubjectCodes[4], student.Biology},
                {SubjectCodes[5], student.CombinedNaturalSciences},
                {SubjectCodes[6], student.History},
                {SubjectCodes[7], student.Geography},
                {SubjectCodes[8], student.CivicEducation},
                {SubjectCodes[9], student.CombinedSocialSciences},
                {SubjectCodes[10], student.English}
            };

            return student;
        }

        private static int? TryParseNullableInt(string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            return null;
        }

        private static double? TryParseNullableDouble(string value)
        {
            if (double.TryParse(value, out double result))
            {
                return result;
            }
            return null;
        }

        public static async Task CommitAsync(string pathFile, int year)
        {
            // Setup one time
            YearNow = year;
            await SetUp();

            using (var reader = new StreamReader(pathFile))
            {
                var bulkData = new List<StudentRaw>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = ParseLineToStudent(line);
                    bulkData.Add(data);

                    if (bulkData.Count >= 150000)
                    {
                        await ImportStudentScoresAsync(bulkData);
                        bulkData.Clear();
                    }
                }

                if (bulkData.Count > 0)
                {
                    await ImportStudentScoresAsync(bulkData);
                }
            }
        }

        public static async Task SetUp()
        {
            if (HashSubject == null)
            {
                HashSubject = new Hashtable();

                using (var context = new DBContextMSS())
                {
                    foreach (var subjectCode in SubjectCodes)
                    {
                        var id = await context.Subjects
                            .Where(x => x.Code == subjectCode)
                            .Select(x => x.Id)
                            .FirstOrDefaultAsync();

                        HashSubject.Add(subjectCode, id);
                    }
                }
            }

            if (HashYear == null)
            {
                HashYear = new Hashtable();
                using (var context = new DBContextMSS())
                {
                    for (int i = 2017; i <= 2024; i++)
                    {
                        var id = await context.SchoolYears
                            .Where(x => x.ExamYear == i)
                            .Select(x => x.Id)
                            .FirstOrDefaultAsync();

                        HashYear.Add(i, id);
                    }
                }
            }
        }

        public static async Task ImportStudentScoresAsync(IEnumerable<StudentRaw> studentCsvs)
        {
            var scoreList = new List<Score>();
            var studentList = new List<Student>();
            uint indexNow = 0;
            using (var context = new DBContextMSS())
            {
                indexNow = (uint)await context.Students.CountAsync();
            }

            foreach (var studentCsv in studentCsvs)
            {
                var studentId = ++indexNow;
                studentList.Add(new Student
                {
                    Id = studentId,
                    StudentCode = studentCsv.ID,
                    SchoolYearId = (uint)HashYear![studentCsv.YearNow!]!,
                    Status = "Active"
                });

                foreach (string subjectCode in SubjectCodes)
                {
                    var score = studentCsv.ScoreTable[subjectCode];
                    if (score != null)
                    {
                        scoreList.Add(new Score
                        {
                            ScoreInDecimal = (double)score,
                            StudentId = studentId,
                            SubjectId = (uint)HashSubject[subjectCode]!
                        });
                    }
                }
            }
            studentCsvs = null;
            await BulkInsertStudents(studentList);
            await MuiltiTaskInsertScores(scoreList);
            studentList = null;
            scoreList = null;

        }

        private static async Task BulkInsertStudents(IEnumerable<Student> studentList)
        {
            using (var context = new DBContextMSS())
            {
                await context.BulkInsertAsync(studentList);
            }
        }

        private static async Task MuiltiTaskInsertScores(IEnumerable<Score> scoreList)
        {
            var commitBatchSize = 100000;
            int pagingScore = (scoreList.Count() / commitBatchSize) + (scoreList.Count() % commitBatchSize == 0 ? 0 : 1);

            using (var context = new DBContextMSS())
            {
                for (int i = 0; i <= pagingScore; i++)
                {
                    await context.BulkInsertAsync(scoreList
                            .Skip(i * commitBatchSize)
                            .Take(commitBatchSize)
                            .ToList());
                }
            }
        }

        public static async Task<List<Statistics>> Statistics()
        {
            List<Statistics> statistics = new();
            using (var context = new DBContextMSS())
            {
                for (int i = 2017; i <= 2021; i++)
                {
                    var listId = await context.Students
                        .Where(s => s.SchoolYearId == (uint)HashYear[i])
                        .Select(s => s.Id)
                        .ToListAsync();

                    var year = new Statistics();
                    year.Year = i;
                    year.StudentCount = (uint)listId.Count();
                    year.Math = (uint)await context.Scores
                        .Where(s => listId.Contains(s.StudentId)
                        && s.SubjectId.Equals(HashSubject[SubjectCodes[0]]))
                        .CountAsync();
                    year.Literature = (uint)await context.Scores
                        .Where(s => listId.Contains(s.StudentId)
                        && s.SubjectId.Equals(HashSubject[SubjectCodes[1]]))
                        .CountAsync();
                    year.Physics = (uint)await context.Scores
                        .Where(s => listId.Contains(s.StudentId)
                        && s.SubjectId.Equals(HashSubject[SubjectCodes[2]]))
                        .CountAsync();
                    year.Chemistry = (uint)await context.Scores
                        .Where(s => listId.Contains(s.StudentId)
                        && s.SubjectId.Equals(HashSubject[SubjectCodes[3]]))
                        .CountAsync();
                    year.Biology = (uint)await context.Scores
                        .Where(s => listId.Contains(s.StudentId)
                        && s.SubjectId.Equals(HashSubject[SubjectCodes[4]]))
                        .CountAsync();
                    year.History = (uint)await context.Scores
                        .Where(s => listId.Contains(s.StudentId)
                        && s.SubjectId.Equals(HashSubject[SubjectCodes[6]]))
                        .CountAsync();
                    year.Geography = (uint)await context.Scores
                        .Where(s => listId.Contains(s.StudentId)
                        && s.SubjectId.Equals(HashSubject[SubjectCodes[7]]))
                        .CountAsync();
                    year.CivicEducation = (uint)await context.Scores
                        .Where(s => listId.Contains(s.StudentId)
                        && s.SubjectId.Equals(HashSubject[SubjectCodes[8]]))
                        .CountAsync();
                    year.English = (uint)await context.Scores
                        .Where(s => listId.Contains(s.StudentId)
                        && s.SubjectId.Equals(HashSubject[SubjectCodes[10]]))
                        .CountAsync();

                    statistics.Add(year);
                }
            }

            return statistics;
        }
    }
}
