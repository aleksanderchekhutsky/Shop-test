
using Microsoft.EntityFrameworkCore;
using Shop.Data.Models;
using Shop.Areas.Identity.Data;


namespace Shop.Data
{
    public class AppDBContent: DbContext 
    {
        public AppDBContent(DbContextOptions<AppDBContent>options) : base(options)
        {
         
        }
        public AppDBContent()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-VSHHQ0D;Database=Shop; Persist Security Info=false; User ID='sa'; Password='sa'; MultipleActiveResultSets=True; Trusted_Connection=False;");
        }
        public DbSet<Car> Car { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<ShopCartItem> ShopCartItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; } 
        public DbSet<WalletTransaction> WalletTransaction { get; set; }
        public DbSet<Wallet> Wallet { get; set; }

    }
}
