using Allsop.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Allsop.DataAccess
{
    public class ShoppingCartDbContext: BaseDbContext<ShoppingCartDbContext>
    {
        public DbSet<ShoppingCartDb> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItemDb> ShoppingCartItems { get; set; }
        public DbSet<UserDb> Users { get; set; }

        public ShoppingCartDbContext(DbContextOptions<ShoppingCartDbContext> options) : base(options)
        {
            GenerateTestData.GenerateShoppingCartData(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ShoppingCart");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShoppingCartItemDb>()
                .HasOne<ShoppingCartDb>().WithMany().HasForeignKey(x => x.ShoppingCartId).HasPrincipalKey(x => x.Id)
                .Metadata.DeleteBehavior = DeleteBehavior.NoAction;

            modelBuilder.Entity<ShoppingCartDb>()
                .HasMany<ShoppingCartItemDb>().WithOne().HasForeignKey(x => x.ShoppingCartId).HasPrincipalKey(x => x.Id)
                .Metadata.DeleteBehavior = DeleteBehavior.NoAction;

            base.OnModelCreating(modelBuilder);
        }
    }
}
