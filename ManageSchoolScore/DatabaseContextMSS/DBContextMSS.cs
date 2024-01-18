using ManageSchoolScore.Models;
using Microsoft.EntityFrameworkCore;

namespace ManageSchoolScore.DatabaseContextMSS
{
    public class DBContextMSS : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ManageSchoolScore;User ID=sa;Password=@1234abc;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        DbSet<Student> Students { get; set; }

        DbSet<Score> Scores { get; set; }

        DbSet<Subject> Subjects { get; set; }

        DbSet<SchoolYear> SchoolYears { get; set; }
    }
}
