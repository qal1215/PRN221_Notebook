using CsvHelper.Configuration.Attributes;

namespace CsvReaderStudentScore.Model
{
    public class StudentRecord
    {
        [Index(0)]
        public string ID { get; set; }

        [Index(1)]
        public string Province { get; set; }

        [Index(2)]
        public double? Mathematics { get; set; } = 0;

        [Index(3)]
        public double? Literature { get; set; } = 0;

        [Index(4)]
        public double? Physics { get; set; } = 0;

        [Index(5)]
        public double? Chemistry { get; set; } = 0;

        [Index(6)]
        public double? Biology { get; set; } = 0;

        [Index(7)]
        public double? CombinedNaturalSciences { get; set; } = 0;

        [Index(8)]
        public double? History { get; set; } = 0;

        [Index(9)]
        public double? Geography { get; set; } = 0;

        [Index(10)]
        public double? CivicEducation { get; set; } = 0;

        [Index(11)]
        public double? CombinedSocialSciences { get; set; } = 0;

        [Index(12)]
        public double? English { get; set; } = 0;

        public StudentRecord(string id, string province, double? mathematics, double? literature, double? physics, double? chemistry, double? biology, double? combinedNaturalSciences, double? history, double? geography, double? civicEducation, double? combinedSocialSciences, double? english)
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
