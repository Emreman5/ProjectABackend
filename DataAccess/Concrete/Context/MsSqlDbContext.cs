using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.Concrete.Context;

public class MsSqlDbContext : IdentityDbContext<CustomUser>
{
    public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : base(options)
    { }

    public DbSet<ApplicationUserToken> AspNetUserTokens { get; set; }
    public DbSet<TestModel> TestModels { get; set; }
    public DbSet<Product> Menus { get; set; }
    public DbSet<MenuImage> MenuImages { get; set; }
    public DbSet<MenuComment> MenuComments{ get; set; } 
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Adress> Adresses { get; set; } 
    public DbSet<MenuComment> Comments { get; set; }
    public DbSet<Reservation> Reservations{ get; set; }
    public DbSet<Category> Categories{ get; set; } 
    






    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}