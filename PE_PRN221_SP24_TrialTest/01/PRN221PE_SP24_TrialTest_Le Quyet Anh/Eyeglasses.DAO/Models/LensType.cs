namespace Eyeglasses.DAO.Models;

public partial class LensType
{
    public string LensTypeId { get; set; } = null!;

    public string LensTypeName { get; set; } = null!;

    public string LensTypeDescription { get; set; } = null!;

    public bool? IsPrescription { get; set; }

    public virtual ICollection<Eyeglass> Eyeglasses { get; set; } = new List<Eyeglass>();
}
