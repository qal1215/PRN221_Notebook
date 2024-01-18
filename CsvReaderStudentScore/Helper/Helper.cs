using CsvHelper;
using System.Globalization;
using System.IO;

namespace CsvReaderStudentScore.Helper
{
    public static class Helper
    {
        public static List<T> ReaderCsv<T>(string path)
        {
            List<T> records = new List<T>();
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var readingResult = csv.GetRecords<T>().ToList();
                records.AddRange(readingResult);
            }

            return records;
        }
    }
}
