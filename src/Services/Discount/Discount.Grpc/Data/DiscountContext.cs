using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions <DiscountContext> options):base(options) { }

        public DbSet<Coupon> Coupons { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasKey(
                    new Coupon { Id = 1, ProductName = "A", Description = "A prod", Amount = 150 },
                    new Coupon { Id = 2, ProductName = "B", Description = "B prod", Amount = 200 }
                );    
        }

    }
}
