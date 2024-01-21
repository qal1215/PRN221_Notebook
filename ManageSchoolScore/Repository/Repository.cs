using ManageSchoolScore.DatabaseContextMSS;
using ManageSchoolScore.Models;
using Microsoft.EntityFrameworkCore;
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

        private static Hashtable HashSubject = new Hashtable();

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

        public static StudentRaw ParseLineToStudent(string line)
        {
            var parts = line.Split(',');

            var student = new StudentRaw
            {
                ID = parts[0],
                Province = parts[1],
                Mathematics = TryParseNullableDouble(parts[2]),
                Literature = TryParseNullableDouble(parts[3]),
                Physics = TryParseNullableDouble(parts[4]),
                Chemistry = TryParseNullableDouble(parts[5]),
                Biology = TryParseNullableDouble(parts[6]),
                CombinedNaturalSciences = TryParseNullableDouble(parts[7]),
                History = TryParseNullableDouble(parts[8]),
                Geography = TryParseNullableDouble(parts[9]),
                CivicEducation = TryParseNullableDouble(parts[10]),
                CombinedSocialSciences = TryParseNullableDouble(parts[11]),
                English = TryParseNullableDouble(parts[12]),
                YearNow = 0,
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

                    if (bulkData.Count >= 50000)
                    {
                        await ImportStudentScoresAsync(bulkData);
                        bulkData.Clear();
                    }
                }

                if (bulkData.Count > 0)
                {
                    await ImportStudentScoresAsync(bulkData);
                    bulkData.Clear();
                }
            }
        }


        private static async Task SetUp()
        {
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

                SchoolYearId = await context.SchoolYears
                    .Where(sy => sy.ExamYear == YearNow)
                    .Select(sy => sy.Id)
                    .FirstOrDefaultAsync();
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
                    SchoolYearId = SchoolYearId,
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

            await BulkInsertStudentNSubject(studentList, scoreList);
            scoreList.Clear();
            studentList.Clear();
        }

        private static async Task BulkInsertStudentNSubject(IEnumerable<Student> studentList, List<Score> scoreList)
        {
            var CommitBatchSize = 50000;
            using (var context = new DBContextMSS())
            {
                await context.BulkInsertAsync(studentList);

                if (scoreList.Count >= CommitBatchSize)
                {
                    int pageIndex = (scoreList.Count / CommitBatchSize) + (scoreList.Count % CommitBatchSize == 0 ? 0 : 1);
                    for (int i = 0; i < pageIndex; i++)
                    {
                        await context.BulkInsertAsync(scoreList
                            .Skip(i * CommitBatchSize)
                            .Take(CommitBatchSize)
                            .ToList());
                    }
                }
                else
                {
                    await context.BulkInsertAsync(scoreList);
                }


            }
        }
    }
}
