using System.ComponentModel.DataAnnotations.Schema;

namespace ManageSchoolScore.Models
{
    public class BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public uint Id { get; set; }
    }
}
