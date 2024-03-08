namespace ManageSchoolScore.Models
{
    public class Score : BaseModel
    {
        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public double ScoreInDecimal { get; set; }

        public Student Student { get; set; } = null!;

        public Subject Subject { get; set; } = null!;
    }
}
