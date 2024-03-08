using System.ComponentModel.DataAnnotations;

namespace ManageSchoolScore.Models
{
    public class Student : BaseModel
    {
        [Required]
        public string StudentCode { get; set; } = null!;

        public int SchoolYearId { get; set; }

        public string Status { get; set; } = "";

        public SchoolYear SchoolYear { get; set; } = null!;
    }
}
