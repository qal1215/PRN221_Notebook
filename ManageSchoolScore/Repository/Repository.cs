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

        private static List<Statistics> statisticsList = new();

        //private static Hashtable TopScores = new();

        private static List<TopScore> topScores = null;

        private static int indexNow = 0;

        public static StudentRaw ParseLineToStudent(string line)
        {
            var parts = line.Split(',');

            var student = new StudentRaw
            {
                ID = parts[0],
                //Mathematics = TryParseNullableDouble(parts[1]),
                //Literature = TryParseNullableDouble(parts[2]),
                //Physics = TryParseNullableDouble(parts[3]),
                //Biology = TryParseNullableDouble(parts[4]),
                //English = TryParseNullableDouble(parts[5]),
                YearNow = TryParseNullableInt(parts[6]),
                //Chemistry = TryParseNullableDouble(parts[7]),
                //History = TryParseNullableDouble(parts[8]),
                //Geography = TryParseNullableDouble(parts[9]),
                //CivicEducation = TryParseNullableDouble(parts[10]),
                Province = parts[11],
            };

            student.ScoreTable = new Hashtable{
                {SubjectCodes[0], TryParseNullableDouble(parts[1])},
                {SubjectCodes[1], TryParseNullableDouble(parts[2])},
                {SubjectCodes[2], TryParseNullableDouble(parts[3])},
                {SubjectCodes[3], TryParseNullableDouble(parts[7])},
                {SubjectCodes[4], TryParseNullableDouble(parts[4])},
                //{SubjectCodes[5], student.CombinedNaturalSciences},
                {SubjectCodes[6], TryParseNullableDouble(parts[8])},
                {SubjectCodes[7], TryParseNullableDouble(parts[9])},
                {SubjectCodes[8], TryParseNullableDouble(parts[10])},
                //{SubjectCodes[9], student.CombinedSocialSciences},
                {SubjectCodes[10], TryParseNullableDouble(parts[5])}
            };

            return student;
        }

        private static int TryParseNullableInt(string value)
        {
            if (int.TryParse(value, out int result))
            {
                return result;
            }
            return 0;
        }

        private static double TryParseNullableDouble(string value)
        {
            if (double.TryParse(value, out double result))
            {
                return result;
            }
            return 0;
        }

        private static DBContextMSS context = new DBContextMSS();

        public static async Task CommitAsync(string pathFile)
        {
            // Setup one time
            await SetUp();

            using (var reader = new StreamReader(pathFile))
            {
                var bulkData = new List<StudentRaw>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var data = ParseLineToStudent(line);

                    StatictisScore(data);


                    bulkData.Add(data);

                    if (bulkData.Count >= 500000)
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
            await SaveStatisticsAsync();
        }

        private static async Task SaveStatisticsAsync()
        {
            await context.BulkInsertAsync(statisticsList);
            await context.BulkInsertAsync(topScores);
        }

        private static void StatictisScore(StudentRaw raw)
        {
            var statictis = statisticsList.Where(s => s.Year == raw.YearNow)
                .FirstOrDefault();

            if (statictis == null)
            {
                statictis = new Statistics();
                statictis.Year = raw.YearNow!.Value;
            }

            statictis.StudentCount++;
            statictis.Math += (double)raw.ScoreTable["MATH"]! > 0 ? 1 : 0;
            statictis.Literature += (double)raw.ScoreTable!["LIT"]! > 0 ? 1 : 0;
            statictis.English += (double)raw.ScoreTable!["ENG"]! > 0 ? 1 : 0;
            statictis.Physics += (double)raw.ScoreTable!["PHYS"]! > 0 ? 1 : 0;
            statictis.Chemistry += (double)raw.ScoreTable!["CHEM"]! > 0 ? 1 : 0;
            statictis.Biology += (double)raw.ScoreTable!["BIO"]! > 0 ? 1 : 0;
            statictis.History += (double)raw.ScoreTable!["HIST"]! > 0 ? 1 : 0;
            statictis.Geography += (double)raw.ScoreTable!["GEO"]! > 0 ? 1 : 0;
            statictis.CivicEducation += (double)raw.ScoreTable!["CIV"]! > 0 ? 1 : 0;

            if (topScores == null)
            {
                topScores = new List<TopScore>();
            }

            double a00 = (double)raw.ScoreTable["MATH"]! + (double)raw.ScoreTable["PHYS"]! + (double)raw.ScoreTable["CHEM"]!;
            var temp = topScores
                .Where(x => x.KhoiThi == "A00" && x.SchoolYear == raw.YearNow)
                .Where(x => x.Score > a00)
                .FirstOrDefault();

            if (temp is null)
            {
                TopScore topA00 = new();
                topA00.SchoolYear = raw.YearNow!.Value;
                topA00.KhoiThi = "A00";
                topA00.Score = a00;
                topA00.StudentCode = raw.ID;
                topScores.RemoveAll(x => x.KhoiThi == "A00" && x.SchoolYear == raw.YearNow && x.Score < a00);
                topScores.Add(topA00);
            }

            double b00 = (double)raw.ScoreTable["MATH"]! + (double)raw.ScoreTable["CHEM"]! + (double)raw.ScoreTable["BIO"]!;

            temp = topScores
                .Where(x => x.KhoiThi == "B00" && x.SchoolYear == raw.YearNow)
                .Where(x => x.Score > b00)
                .FirstOrDefault();

            if (temp is null)
            {
                TopScore topB00 = new();
                topB00.SchoolYear = raw.YearNow!.Value;
                topB00.KhoiThi = "B00";
                topB00.Score = b00;
                topB00.StudentCode = raw.ID;
                topScores.RemoveAll(x => x.KhoiThi == "B00" && x.SchoolYear == raw.YearNow && x.Score < b00);
                topScores.Add(topB00);
            }

            double c00 = (double)raw.ScoreTable["LIT"]! + (double)raw.ScoreTable["HIST"]! + (double)raw.ScoreTable["GEO"]!;
            temp = topScores
                .Where(x => x.KhoiThi == "C00" && x.SchoolYear == raw.YearNow)
                .Where(x => x.Score > c00)
                .FirstOrDefault();
            if (temp is null)
            {
                TopScore topC00 = new();
                topC00.SchoolYear = raw.YearNow!.Value;
                topC00.KhoiThi = "C00";
                topC00.Score = c00;
                topC00.StudentCode = raw.ID;

                topScores.RemoveAll(x => x.KhoiThi == "C00" && x.SchoolYear == raw.YearNow && x.Score < c00);
                topScores.Add(topC00);
            }

            double d01 = (double)raw.ScoreTable["MATH"]! + (double)raw.ScoreTable["LIT"]! + (double)raw.ScoreTable["ENG"]!;
            temp = topScores
                .Where(x => x.KhoiThi == "D01" && x.SchoolYear == raw.YearNow)
                .Where(x => x.Score > d01)
                .FirstOrDefault();

            if (temp is null)
            {
                TopScore topD01 = new();
                topD01.SchoolYear = raw.YearNow!.Value;
                topD01.KhoiThi = "D01";
                topD01.Score = d01;
                topD01.StudentCode = raw.ID;

                topScores.RemoveAll(x => x.KhoiThi == "D01" && x.SchoolYear == raw.YearNow && x.Score < d01);
                topScores.Add(topD01);
            }

            double a01 = (double)raw.ScoreTable["MATH"]! + (double)raw.ScoreTable["PHYS"]! + (double)raw.ScoreTable["ENG"]!;
            temp = topScores
                .Where(x => x.KhoiThi == "A01" && x.SchoolYear == raw.YearNow)
                .Where(x => x.Score > a01)
                .FirstOrDefault();
            if (temp is null)
            {
                TopScore topA01 = new();
                topA01.SchoolYear = raw.YearNow!.Value;
                topA01.KhoiThi = "A01";
                topA01.Score = a01;
                topA01.StudentCode = raw.ID;

                topScores.RemoveAll(x => x.KhoiThi == "A01" && x.SchoolYear == raw.YearNow && x.Score < a01);
                topScores.Add(topA01);
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

            for (int i = 2017; i <= 2021; i++)
            {
                statisticsList.Add(new Statistics
                {
                    Year = i
                });
            }
        }

        public static async Task ImportStudentScoresAsync(List<StudentRaw> studentCsvs)
        {
            List<Score> scoreList = new();
            List<Student> studentList = new();

            foreach (var studentCsv in studentCsvs)
            {
                var studentId = ++indexNow;
                studentList.Add(new Student
                {
                    Id = studentId,
                    StudentCode = studentCsv.ID,
                    SchoolYearId = (int)HashYear![studentCsv.YearNow!]!,
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
                            SubjectId = (int)HashSubject![subjectCode]!
                        });
                    }
                }
            }

            await BulkInsertStudents(studentList);
            await BulkInsertScores(scoreList);
            studentList.Clear();
            scoreList.Clear();
            studentCsvs.Clear();
        }

        private static async Task BulkInsertStudents(IEnumerable<Student> studentList)
        {
            await context.BulkInsertAsync(studentList);
        }

        private static async Task BulkInsertScores(List<Score> scoreList)
        {
            int batchSize = 1000000;
            int page = (scoreList.Count / batchSize) + (scoreList.Count % batchSize == 0 ? 0 : 1);

            for (int i = 0; i <= page; i++)
            {
                await context.BulkInsertAsync(scoreList
                    .Skip(i * batchSize)
                    .Take(batchSize));
            }
        }

        public static async Task<List<Statistics>> Statistics()
        {
            List<Statistics> statistics = new();

            for (int i = 2017; i <= 2021; i++)
            {
                var statistic = await context.Statistics
                    .Where(x => x.Year == i)
                    .FirstOrDefaultAsync();
                statistics.Add(statistic!);
            }

            return statistics;
        }

        public static async Task<List<TopScore>?> GetTopScores(int schoolYear)
        {
            //List<TopScore> topScores = new();

            return await context.TopScores
                .Where(x => x.SchoolYear == schoolYear)
                .ToListAsync();

        }
    }
}
