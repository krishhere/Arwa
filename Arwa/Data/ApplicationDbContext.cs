using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ProdManager> ProdManagers { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<WaterCan> WaterCans { get; set; }
    public DbSet<Billing> Billings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProdManager>(b =>
        {
            b.HasKey(x => x.ProdManagerId);
            b.Property(x => x.Name).HasMaxLength(50);
            b.Property(x => x.Phone).HasMaxLength(10);
            b.Property(x => x.Email).HasMaxLength(100);
            b.Property(x => x.Location).HasMaxLength(250);
        });

        modelBuilder.Entity<Vendor>(b =>
        {
            b.HasKey(x => x.VendorId);
            b.Property(x => x.Name).HasMaxLength(50);
            b.Property(x => x.Phone).HasMaxLength(10);
            b.Property(x => x.Email).HasMaxLength(100);
            b.Property(x => x.Location).HasMaxLength(250);
        });

        modelBuilder.Entity<WaterCan>(b =>
        {
            b.HasKey(x => x.WaterCanId);
            b.HasOne(x => x.ProdManager).WithMany(p => p.WaterCans).HasForeignKey(x => x.ProdManagerId);
            b.HasOne(x => x.Vendor).WithMany(v => v.WaterCans).HasForeignKey(x => x.VendorId);
        });

        modelBuilder.Entity<Billing>(b =>
        {
            b.HasKey(x => x.BillingId);
            b.HasOne(x => x.WaterCan).WithMany(w => w.Billings).HasForeignKey(x => x.WaterCanId);
            b.Property(x => x.AmountBilled).HasColumnType("decimal(10,2)");
            b.Property(x => x.AmountPaid).HasColumnType("decimal(10,2)");
            b.Property(x => x.PriceQuotedToVendor).HasColumnType("decimal(10,2)");
        });
    }
}
