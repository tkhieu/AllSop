using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Mapper;
using Allsop.Service.Contract.Model;
using NUnit.Framework;

namespace Allsop.DataAccess.Repository.Tests.Mapper
{
    [TestFixture]
    public class ShoppingCartItemMapperTests
    {
        [Test]
        public void Should_Be_Injected()
        {
            // Arrange
            var serviceProvider = DependencyInjector.GetServiceProvider();
            var mapperFactory = serviceProvider.GetService(typeof(IMapperFactory)) as IMapperFactory;

            // Act
            var mapper = mapperFactory.GetMapper<ShoppingCartItem, ShoppingCartItemDb>() as ShoppingCartItemMapper;

            // Assert
            Assert.IsNotNull(mapper);
        }

        [Test]
        public void ShoppingCartItemMapper_MapEntityToModel_ShouldMapCorrectly()
        {
            var entity = new ShoppingCartItemDb()
            {
                Id = Guid.NewGuid(),
                Quantity = 1,
                ProductId = Guid.NewGuid(),
                ShoppingCartId = Guid.NewGuid()
            };

            var serviceProvider = DependencyInjector.GetServiceProvider();
            var mapperFactory = serviceProvider.GetService(typeof(IMapperFactory)) as IMapperFactory;
            var mapper = new ShoppingCartItemMapper(mapperFactory);
            var model = mapper.From(entity);

            Assert.IsNotNull(model);
            Assert.AreEqual(entity.Id, model.Id);
            Assert.AreEqual(entity.Quantity, model.Quantity);
            Assert.AreEqual(entity.ProductId, model.ProductId);
            Assert.AreEqual(entity.ShoppingCartId, model.ShoppingCartId);
        }

        [Test]
        public void ShoppingCartItemMapper_MapModelToEntity_ShouldMapCorrectly()
        {
            var model = new ShoppingCartItem()
            {
                Id = Guid.NewGuid(),
                Quantity = 1,
                ProductId = Guid.NewGuid(),
                ShoppingCartId = Guid.NewGuid()
            };

            var serviceProvider = DependencyInjector.GetServiceProvider();
            var mapperFactory = serviceProvider.GetService(typeof(IMapperFactory)) as IMapperFactory;
            var mapper = new ShoppingCartItemMapper(mapperFactory);
            var entity = mapper.From(model);

            Assert.IsNotNull(entity);
            Assert.AreEqual(model.Id, entity.Id);
            Assert.AreEqual(model.Quantity, entity.Quantity);
            Assert.AreEqual(model.ProductId, entity.ProductId);
            Assert.AreEqual(model.ShoppingCartId, entity.ShoppingCartId);
        }
    }
}
