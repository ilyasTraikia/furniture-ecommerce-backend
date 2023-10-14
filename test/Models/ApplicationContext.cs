using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace test.Models;


public class ApplicationContext : IdentityDbContext<IdentityUser>
{
    public ApplicationContext(DbContextOptions <ApplicationContext> options)
        :base(options) 
    { 
    
    }


    

   public DbSet<Product> Products { get; set; } = null!;
  //  public DbSet<Cart> Cart { get; set; } = null!;
  //  public DbSet<Cart_Items> Cart_Items { get; set; } = null!;
//    public DbSet<Order> Order { get; set; } = null!;


 //   protected override void OnModelCreating(ModelBuilder modelBuilder)
 //   {
 //       base.OnModelCreating(modelBuilder);
 //       modelBuilder.Entity<Cart_Items>()
//              .HasKey(m => new { m.CartIdRef, m.ProductIdRef });
//    }






}

