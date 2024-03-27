using Microsoft.EntityFrameworkCore;

namespace SignalRLab.Models
{
    public class ApplicationDbContextSignalR : DbContext
    {
        public ApplicationDbContextSignalR(DbContextOptions<ApplicationDbContextSignalR> options) : base(options)
        {
        }

        public ApplicationDbContextSignalR()
        {
        }

        public virtual DbSet<Product> Products { get; set; }
    }
}
