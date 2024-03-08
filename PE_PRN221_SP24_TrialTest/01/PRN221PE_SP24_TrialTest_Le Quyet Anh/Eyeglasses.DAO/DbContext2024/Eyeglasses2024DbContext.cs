using Eyeglasses.DAO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eyeglasses.DAO.DbContext2024;

public partial class Eyeglasses2024DbContext : DbContext
{
    public Eyeglasses2024DbContext()
    {
    }

    public Eyeglasses2024DbContext(DbContextOptions<Eyeglasses2024DbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Eyeglass> Eyeglasses { get; set; }

    public virtual DbSet<LensType> LensTypes { get; set; }

    public virtual DbSet<StoreAccount> StoreAccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config.GetConnectionString("DefaultConnection");
        return strConn!;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Eyeglass>(entity =>
        {
            entity.HasKey(e => e.EyeglassesId).HasName("PK__Eyeglass__93A83C7BBB33B255");

            entity.Property(e => e.EyeglassesId).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EyeglassesDescription).HasMaxLength(250);
            entity.Property(e => e.EyeglassesName).HasMaxLength(100);
            entity.Property(e => e.FrameColor).HasMaxLength(50);
            entity.Property(e => e.LensTypeId).HasMaxLength(30);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.LensType).WithMany(p => p.Eyeglasses)
                .HasForeignKey(d => d.LensTypeId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Eyeglasse__LensT__29572725");
        });

        modelBuilder.Entity<LensType>(entity =>
        {
            entity.HasKey(e => e.LensTypeId).HasName("PK__LensType__D6DC1FE62A46937F");

            entity.ToTable("LensType");

            entity.Property(e => e.LensTypeId).HasMaxLength(30);
            entity.Property(e => e.LensTypeDescription).HasMaxLength(250);
            entity.Property(e => e.LensTypeName).HasMaxLength(100);
        });

        modelBuilder.Entity<StoreAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__StoreAcc__349DA586F113E240");

            entity.ToTable("StoreAccount");

            entity.HasIndex(e => e.EmailAddress, "UQ__StoreAcc__49A147404E2764ED").IsUnique();

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("AccountID");
            entity.Property(e => e.AccountPassword).HasMaxLength(40);
            entity.Property(e => e.EmailAddress).HasMaxLength(60);
            entity.Property(e => e.FullName).HasMaxLength(60);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
