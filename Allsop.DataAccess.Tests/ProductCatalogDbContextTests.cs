using Allsop.DataAccess.Model;
using Allsop.Tests.Helpers;
using NUnit.Framework;

namespace Allsop.DataAccess.Tests
{
    [TestFixture]
    public class ProductCatalogDbContextTests
    {
        [Test]
        public async Task AddProducts_WithValidData_ShouldSaveSuccessfullyAsync()
        {
            var id = Guid.NewGuid();
            var name = "Product 1";
            var price = 11.3m;
            var quantity = 10;
            var categoryId = Guid.NewGuid();

            using var context = CreateDbContext();

            context.Products.Add(new ProductDb() { Id = id, Name = name, Price = price, Quantity = quantity, CategoryId = categoryId });
            await context.SaveChangesAsync();

            var product = context.Products.FirstOrDefault(x=>x.Id == id);

            Assert.IsNotNull(product);
            Assert.AreEqual(id, product.Id);
            Assert.AreEqual(name, product.Name);
            Assert.AreEqual(price, product.Price);
            Assert.AreEqual(quantity, product.Quantity);
            Assert.AreEqual(categoryId, product.CategoryId);
        }

        [Test]
        public async Task UpdateProducts_WithValidData_ShouldSaveSuccessfullyAsync()
        {
            var id = Guid.NewGuid();
            var name = "Product 1";
            var price = 11.3m;
            var quantity = 10;
            var categoryId = Guid.NewGuid();

            using (var context = CreateDbContext())
            {
                context.Products.Add(new ProductDb() { Id = id, Name = name, Price = price, Quantity = quantity, CategoryId = categoryId });
                await context.SaveChangesAsync();
            }

            using (var context = CreateDbContext())
            {
                var product = context.Products.FirstOrDefault(x => x.Id == id);

                product.Name = "Product 2";
                product.Quantity = 1;
                product.Price = 2m;

                await context.SaveChangesAsync();
            }

            using (var context = CreateDbContext())
            {
                var product = context.Products.FirstOrDefault(x => x.Id == id);

                Assert.IsNotNull(product);
                Assert.AreEqual("Product 2", product.Name);
                Assert.AreEqual(2m, product.Price);
                Assert.AreEqual(1, product.Quantity);
            }
        }

        [Test]
        public async Task DeleteProducts_WithValidData_ShouldDeleteSuccessfullyAsync()
        {
            var id = Guid.NewGuid();
            var name = "Product 1";
            var price = 11.3m;
            var quantity = 10;
            var categoryId = Guid.NewGuid();

            using (var context = CreateDbContext())
            {
                context.Products.Add(new ProductDb() { Id = id, Name = name, Price = price, Quantity = quantity, CategoryId = categoryId });
                await context.SaveChangesAsync();
            }

            using (var context = CreateDbContext())
            {
                var product = context.Products.FirstOrDefault(x => x.Id == id);

                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }

            using (var context = CreateDbContext())
            {
                var product = context.Products.FirstOrDefault(x => x.Id == id);

                Assert.IsNull(product);
            }
        }

        [Test]
        public void AddProducts_WithDuplicateId_ShouldThrowException()
        {
            var id = Guid.NewGuid();
            using var context = CreateDbContext();

            context.Products.Add(new ProductDb() { Id = id, Name = "Product 1" });
            Assert.Throws<InvalidOperationException>(() =>
                context.Products.Add(new ProductDb() { Id = id, Name = "Product 2" }));
        }

        private ProductCatalogDbContext CreateDbContext()
        {
            return DbContextFactory.CreateDbContext<ProductCatalogDbContext>("ProductCatalogTest");
        }
    }
}
