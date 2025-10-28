using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<ProdManager> ProdManagers { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<WaterCanOrders> WaterCanOrders { get; set; }
    public DbSet<Billing> Billings { get; set; }

}