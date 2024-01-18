namespace ManageSchoolScore.Models
{
    public class SchoolYear : BaseModel
    {
        public string Name { get; set; } = "";

        public int ExamYear { get; set; }

        public string Status { get; set; } = "";
    }
}
