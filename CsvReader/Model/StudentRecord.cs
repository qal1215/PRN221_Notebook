namespace CsvReaderStudentScore.Model
{
    public class StudentRecord
    {
        public string ID { get; set; }
        public string Province { get; set; }
        public double Mathematics { get; set; }
        public double Literature { get; set; }
        public double Physics { get; set; }
        public double Chemistry { get; set; }
        public double Biology { get; set; }
        public double CombinedNaturalSciences { get; set; }
        public double History { get; set; }
        public double Geography { get; set; }
        public double CivicEducation { get; set; }
        public double CombinedSocialSciences { get; set; }
        public double English { get; set; }

        public StudentRecord(string id, string province, double mathematics, double literature, double physics, double chemistry, double biology, double combinedNaturalSciences, double history, double geography, double civicEducation, double combinedSocialSciences, double english)
        {
            ID = id;
            Province = province;
            Mathematics = mathematics;
            Literature = literature;
            Physics = physics;
            Chemistry = chemistry;
            Biology = biology;
            CombinedNaturalSciences = combinedNaturalSciences;
            History = history;
            Geography = geography;
            CivicEducation = civicEducation;
            CombinedSocialSciences = combinedSocialSciences;
            English = english;
        }

        public StudentRecord()
        {
        }
    }
}
