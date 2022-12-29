using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DataAccess.Concrete.Context;

public class MsSqlDbContext : IdentityDbContext<CustomUser>
{
    public MsSqlDbContext(DbContextOptions<MsSqlDbContext> options) : base(options)
    { }

    DbSet<TestModel> TestModels { get; set; }
    DbSet<Product> Menus { get; set; }
    DbSet<MenuImage> MenuImages { get; set; }
    DbSet<MenuComment> MenuComments{ get; set; } 
    DbSet<Order> Orders { get; set; }
    DbSet<OrderDetail> OrderDetails { get; set; }
    DbSet<Adress> Adresses { get; set; }
    DbSet<MenuComment> Comments { get; set; }
    DbSet<Reservation> Reservations{ get; set; }
    DbSet<Category> Categories{ get; set; }







    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}