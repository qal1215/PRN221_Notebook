using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ManageSchoolScore.CsvHelper
{
    public class CsvHelping
    {
        public static IList<T> ReaderCsv<T>(string path)
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
