using System.ComponentModel.DataAnnotations.Schema;

namespace ManageSchoolScore.Models
{
    public class Statistics
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Year { get; set; }

        public int StudentCount { get; set; }

        public int Math { get; set; }

        public int Literature { get; set; }

        public int Physics { get; set; }

        public int Biology { get; set; }

        public int English { get; set; }

        public int Chemistry { get; set; }

        public int History { get; set; }

        public int Geography { get; set; }

        public int CivicEducation { get; set; }
    }
}
