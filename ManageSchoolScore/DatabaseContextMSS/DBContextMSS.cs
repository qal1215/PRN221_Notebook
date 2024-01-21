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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subject>().HasData(SubjectSeed.Subjects);

            modelBuilder.Entity<SchoolYear>().HasData(SchoolYearSeed.SchoolYears);
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Score> Scores { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<SchoolYear> SchoolYears { get; set; }

        public DbSet<StudentRaw> StudentCsvs { get; set; }
    }
}
