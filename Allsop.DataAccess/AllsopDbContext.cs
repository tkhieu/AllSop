using Allsop.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Allsop.DataAccess
{
    public class AllsopDbContext: BaseDbContext
    {
        public DbSet<CategoryDb> Categories { get; set; }
        public DbSet<ProductDb> Products { get; set; }
        public DbSet<ShoppingCartDb> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartItemDb> ShoppingCartItems { get; set; }
        public DbSet<UserDb> Users { get; set; }
        public DbSet<PromotionDb> Promotions { get; set; }

        public AllsopDbContext(DbContextOptions<AllsopDbContext> options) : base(options)
        {
            GenerateTestData.GenerateData(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromotionDb>()
                .HasOne(x => x.Category);
                
            modelBuilder.Entity<ProductDb>()
                .HasOne(x => x.Category)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.NoAction;
            
            modelBuilder.Entity<ShoppingCartItemDb>()
                .HasOne<ShoppingCartDb>().WithMany().HasForeignKey(x=>x.ShoppingCartId).HasPrincipalKey(x=>x.Id)
                .Metadata.DeleteBehavior = DeleteBehavior.NoAction;

            modelBuilder.Entity<ShoppingCartDb>()
                .HasOne(x => x.User);

            modelBuilder.Entity<ShoppingCartDb>()
                .HasMany<ShoppingCartItemDb>().WithOne().HasForeignKey(x=>x.ShoppingCartId).HasPrincipalKey(x=>x.Id)
                .Metadata.DeleteBehavior = DeleteBehavior.NoAction;

            
            base.OnModelCreating(modelBuilder);
        }
    }
}
