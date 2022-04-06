using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Mapper;
using Allsop.Service.Contract.Model;
using NUnit.Framework;

namespace Allsop.DataAccess.Repository.Tests.Mapper
{
    [TestFixture]
    public class ShoppingCartMapperTests
    {
        [Test]
        public void Should_Be_Injected()
        {
            // Arrange
            var serviceProvider = DependencyInjector.GetServiceProvider();
            var mapperFactory = serviceProvider.GetService(typeof(IMapperFactory)) as IMapperFactory;

            // Act
            var mapper = mapperFactory.GetMapper<ShoppingCart, ShoppingCartDb>() as ShoppingCartMapper;

            // Assert
            Assert.IsNotNull(mapper);
        }

        [Test]
        public void ShoppingCartMapper_MapEntityToModel_ShouldMapCorrectly()
        {
            var entity = new ShoppingCartDb()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                TotalItems = 10,
                ShoppingCartItems = new List<ShoppingCartItemDb>()
                {
                    new ShoppingCartItemDb() { Id = Guid.NewGuid() }, 
                    new ShoppingCartItemDb() { Id = Guid.NewGuid() }
                }
            };

            var serviceProvider = DependencyInjector.GetServiceProvider();
            var mapperFactory = serviceProvider.GetService(typeof(IMapperFactory)) as IMapperFactory;
            var mapper = new ShoppingCartMapper(mapperFactory);
            var model = mapper.From(entity);

            Assert.IsNotNull(model);
            Assert.AreEqual(entity.Id, model.Id);
            Assert.AreEqual(entity.UserId, model.UserId);
            Assert.AreEqual(2, model.ShoppingCartItems.Count());
        }

        [Test]
        public void ShoppingCartMapper_MapModelToEntity_ShouldMapCorrectly()
        {
            var model = new ShoppingCart()
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                ShoppingCartItems = new List<ShoppingCartItem>()
                {
                    new ShoppingCartItem() { Id = Guid.NewGuid() },
                    new ShoppingCartItem() { Id = Guid.NewGuid() }
                }
            };

            var serviceProvider = DependencyInjector.GetServiceProvider();
            var mapperFactory = serviceProvider.GetService(typeof(IMapperFactory)) as IMapperFactory;
            var mapper = new ShoppingCartMapper(mapperFactory);
            var entity = mapper.From(model);

            Assert.IsNotNull(model);
            Assert.AreEqual(model.Id, entity.Id);
            Assert.AreEqual(model.UserId, entity.UserId);
            Assert.AreEqual(2, entity.ShoppingCartItems.Count());
        }
    }
}
