using Allsop.Common.Exception;
using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Mapper;
using Allsop.DataAccess.Repository.Repositories;
using Allsop.Service.Contract.Model;
using Allsop.Tests.Helpers;
using Moq;
using NUnit.Framework;

namespace Allsop.DataAccess.Repository.Tests.Repositories
{
    [TestFixture]
    public class ProductRepositoryTests
    {
        private ProductCatalogDbContext _dbContext;
        private readonly Guid _id1 = Guid.NewGuid();
        private readonly Guid _id2 = Guid.NewGuid();
        private readonly Guid _id3 = Guid.NewGuid();
        private readonly Guid _id4 = Guid.NewGuid();

        [SetUp]
        public async Task Setup()
        {
            _dbContext = DbContextFactory.CreateDbContext<ProductCatalogDbContext>(Guid.NewGuid().ToString());
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task ProductRepository_GetAllProducts_ShouldReturnDataAsync()
        {
            await InitData();
            var repository = CreateProductRepository(_dbContext);
            var products = await repository.GetAllProductsAsync();

            Assert.IsNotNull(products);
        }

        [Test]
        public async Task CreateProduct_WithValidData_ShouldCreateSuccessfullyAsync()
        {
            var id = _id3;
            var name = "New Product";
            var categoryId = Guid.NewGuid();
            var repository = CreateProductRepository(DbContextFactory.CreateDbContext<ProductCatalogDbContext>(Guid.NewGuid().ToString()));
            var product = await repository.CreateProductAsync(new Product() { Id = id, Name = name, CategoryId = categoryId, Category = new Category() { Id = categoryId, Name = "Test" } });

            Assert.IsNotNull(product);
            Assert.AreEqual(id, product.Id);
            Assert.AreEqual(name, product.Name);
        }

        [Test]
        public Task CreateProduct_WithEmptyId_ShouldThrowExceptionAsync()
        {
            var repository = CreateProductRepository(DbContextFactory.CreateDbContext<ProductCatalogDbContext>(Guid.NewGuid().ToString()));
            Assert.ThrowsAsync<InvalidArgumentException>(() => repository.CreateProductAsync(new Product() { Id = Guid.Empty }));
            return Task.CompletedTask;
        }

        [Test]
        public async Task CreateProducts_WithValidData_ShouldCreateSuccessfullyAsync()
        {
            var categoryId = Guid.NewGuid();
            var repository = CreateProductRepository(DbContextFactory.CreateDbContext<ProductCatalogDbContext>(Guid.NewGuid().ToString()));
            var products = await repository.CreateProductsAsync(new List<Product>()
            {
                new Product(){Id = Guid.NewGuid(), Name = "Product 1", CategoryId = categoryId, Category = new Category(){Id = categoryId , Name = "Test"} },
                new Product(){Id = Guid.NewGuid(), Name = "Product 2", CategoryId = categoryId, Category = new Category(){Id = categoryId , Name = "Test"} },
                new Product(){Id = Guid.NewGuid(), Name = "Product 3", CategoryId = categoryId, Category = new Category(){Id = categoryId , Name = "Test"} },
            });

            Assert.IsNotNull(products);
            Assert.AreEqual(3, products.Count());
        }

        [Test]
        public async Task ProductRepository_DeleteProduct_ShouldDeleteSuccessfullyAsync()
        {
            var id = _id4;

            _dbContext.Products.Add(new ProductDb() { Id = id, Name = "Product 3", CategoryId = Guid.NewGuid() });
            await _dbContext.SaveChangesAsync();

            var repository = CreateProductRepository(DbContextFactory.CreateDbContext<ProductCatalogDbContext>(Guid.NewGuid().ToString()));
            var result = await repository.DeleteProductAsync(id);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.Success);
        }

        private async Task InitData()
        {
            var categoryId = Guid.NewGuid();
            _dbContext.Categories.Add(new CategoryDb() { Id = categoryId, Name = "Category" });
            _dbContext.Products.AddRange(new List<ProductDb>()
            {
                new ProductDb() { Id = _id1, Name = "Product 1", CategoryId = categoryId},
                new ProductDb() { Id = _id2, Name = "Product 2", CategoryId = categoryId}
            });

            await _dbContext.SaveChangesAsync();
        }

        private ProductRepository CreateProductRepository(ProductCatalogDbContext dbContext)
        {
            var mapperFactoryMock = new Mock<IMapperFactory>();

            mapperFactoryMock.Setup(m => m.GetMapper<Product, ProductDb>()).Returns(new ProductMapper(mapperFactoryMock.Object));

            return new ProductRepository(dbContext, mapperFactoryMock.Object);
        }
    }
}
