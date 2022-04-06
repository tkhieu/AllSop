using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Mapper;
using Allsop.Service.Contract.Model;
using NUnit.Framework;

namespace Allsop.DataAccess.Repository.Tests.Mapper
{
    [TestFixture]
    public class ProductMapperTests
    {
        [Test]
        public void Should_Be_Injected()
        {
            // Arrange
            var serviceProvider = DependencyInjector.GetServiceProvider();
            var mapperFactory = serviceProvider.GetService(typeof(IMapperFactory)) as IMapperFactory;

            // Act
            var mapper = mapperFactory.GetMapper<Product, ProductDb>() as ProductMapper;

            // Assert
            Assert.IsNotNull(mapper);
        }

        [Test]
        public void ProductMapper_MapEntityToModel_ShouldMapCorrectly()
        {
            var entity = new ProductDb()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Quantity = 1,
                Price = 10m,
                CategoryId = Guid.NewGuid()
            };

            var serviceProvider = DependencyInjector.GetServiceProvider();
            var mapperFactory = serviceProvider.GetService(typeof(IMapperFactory)) as IMapperFactory;
            var mapper = new ProductMapper(mapperFactory);

            var model = mapper.From(entity);

            Assert.IsNotNull(model);
            Assert.AreEqual(entity.Id, model.Id);
            Assert.AreEqual(entity.Name, model.Name);
            Assert.AreEqual(entity.Quantity, model.Quantity);
            Assert.AreEqual(entity.Price, model.Price); 
            Assert.AreEqual(entity.CategoryId, model.CategoryId);
        }

        [Test]
        public void ProductMapper_MapModelToEntity_ShouldMapCorrectly()
        {
            var model = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Product 1",
                Quantity = 1,
                Price = 10m,
                CategoryId = Guid.NewGuid()
            };

            var serviceProvider = DependencyInjector.GetServiceProvider();
            var mapperFactory = serviceProvider.GetService(typeof(IMapperFactory)) as IMapperFactory;
            var mapper = new ProductMapper(mapperFactory);

            var entity = mapper.From(model);

            Assert.IsNotNull(entity);
            Assert.AreEqual(model.Id, entity.Id);
            Assert.AreEqual(model.Name, entity.Name);
            Assert.AreEqual(model.Quantity, entity.Quantity);
            Assert.AreEqual(model.Price, entity.Price);
            Assert.AreEqual(model.CategoryId, entity.CategoryId);
        }
    }
}
