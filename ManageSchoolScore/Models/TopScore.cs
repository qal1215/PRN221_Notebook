using System.ComponentModel.DataAnnotations.Schema;

namespace ManageSchoolScore.Models
{
    public class TopScore
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string StudentCode { get; set; } = null!;

        public int SchoolYear { get; set; }

        public string KhoiThi { get; set; }

        public double Score { get; set; }
    }
}
