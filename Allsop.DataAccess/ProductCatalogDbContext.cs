using Allsop.DataAccess.Model;
using Microsoft.EntityFrameworkCore;

namespace Allsop.DataAccess
{
    public class ProductCatalogDbContext: BaseDbContext<ProductCatalogDbContext>
    {
        public DbSet<CategoryDb> Categories { get; set; }
        public DbSet<ProductDb> Products { get; set; }

        public ProductCatalogDbContext(DbContextOptions<ProductCatalogDbContext> options) : base(options)
        {
            GenerateTestData.GenerateProductCategoryData(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("ProductCatalog");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDb>()
                .HasOne(x => x.Category)
                .WithMany()
                .Metadata.DeleteBehavior = DeleteBehavior.NoAction;
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
