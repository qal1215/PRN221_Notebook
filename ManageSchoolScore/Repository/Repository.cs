using ManageSchoolScore.DatabaseContextMSS;
using ManageSchoolScore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageSchoolScore.Repository
{
    public class Repository
    {
        private readonly DBContextMSS _dBContextMSS;
        public int CommitBatchSize { get; set; } = 70000;
        public List<StudentCsv> InternalStore { get; set; } = null!;


        public Repository()
        {
            _dBContextMSS = new DBContextMSS();
        }

        public async Task CommitAsync()
        {
            if (InternalStore.Count > 0)
            {
                int numberOfPages = (InternalStore.Count / CommitBatchSize) + (InternalStore.Count % CommitBatchSize == 0 ? 0 : 1);
                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    var dt = InternalStore!
                        .Skip(pageIndex * CommitBatchSize)
                        .Take(CommitBatchSize)
                        .ToList();

                    var dt2 = InternalStore!
                        .Skip((++pageIndex) * CommitBatchSize)
                        .Take(CommitBatchSize)
                        .ToList();

                    var dt3 = InternalStore!
                        .Skip((++pageIndex) * CommitBatchSize)
                        .Take(CommitBatchSize)
                        .ToList();

                    await InsertDataAsync(dt);
                    await InsertDataAsync(dt2);
                    await InsertDataAsync(dt3);
                    await _dBContextMSS.SaveChangesAsync();
                }
            }
        }

        public async Task InsertDataAsync(IList<StudentCsv> data)
        {
            await _dBContextMSS.StudentCsvs.AddRangeAsync(data);
        }
    }
}
