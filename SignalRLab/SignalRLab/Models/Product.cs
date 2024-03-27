using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalRLab.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProId { get; set; }

        public string ProName { get; set; }

        public string Category { get; set; }

        public decimal UnitPrice { get; set; }

        public int StockQty { get; set; }
    }
}
