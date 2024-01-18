namespace ManageSchoolScore.Models
{
    public class Score : BaseModel
    {
        public uint StudentId { get; set; }

        public uint SubjectId { get; set; }

        public double ScoreInDecimal { get; set; }

        public Student Student { get; set; } = null!;

        public Subject Subject { get; set; } = null!;
    }
}
