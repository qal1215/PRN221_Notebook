using System.Globalization;
using System.IO;

namespace CsvReaderStudentScore.Helper
{
    public static class CsvReading
    {
        public static List<T> ReadCsv<T>(string path)
        {
            List<T> studentRecords = new List<T>();
            using (var reader = new StreamReader(path))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();
                    while (csv.Read())
                    {
                        var record = csv.GetRecord<T>();
                        studentRecords.Add(record);
                    }
                }
            }
            return studentRecords;
        }
    }
}
