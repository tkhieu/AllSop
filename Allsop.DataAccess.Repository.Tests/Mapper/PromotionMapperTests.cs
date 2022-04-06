using Allsop.Common.Enums;
using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Mapper;
using Allsop.Service.Contract.Model;
using NUnit.Framework;

namespace Allsop.DataAccess.Repository.Tests.Mapper
{
    [TestFixture]
    public class PromotionMapperTests
    {
        [Test]
        public void Should_Be_Injected()
        {
            // Arrange
            var serviceProvider = DependencyInjector.GetServiceProvider();
            var mapperFactory = serviceProvider.GetService(typeof(IMapperFactory)) as IMapperFactory;

            // Act
            var mapper = mapperFactory.GetMapper<Promotion, PromotionDb>() as PromotionMapper;

            // Assert
            Assert.IsNotNull(mapper);
        }

        [Test]
        public void PromotionMapper_MapEntityToModel_ShouldMapCorrectly()
        {
            var entity = new PromotionDb()
            {
                Id = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                DiscountAmount = 10.5m,
                SpendAmount = 100m,
                PromotionType = PromotionType.Percent,
                DiscountPercent = 15m
            };

            var mapper = new PromotionMapper();

            var model = mapper.From(entity);

            Assert.IsNotNull(model);
            Assert.AreEqual(entity.Id, model.Id);
            Assert.AreEqual(entity.CategoryId, model.CategoryId);
            Assert.AreEqual(entity.DiscountAmount, model.DiscountAmount);
            Assert.AreEqual(entity.SpendAmount, model.SpendAmount);
            Assert.AreEqual(entity.PromotionType, model.PromotionType);
            Assert.AreEqual(entity.DiscountPercent, model.DiscountPercent);
        }

        [Test]
        public void PromotionMapper_MapModelToEntity_ShouldMapCorrectly()
        {
            var model = new Promotion()
            {
                Id = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(),
                DiscountAmount = 10.5m,
                SpendAmount = 100m,
                PromotionType = PromotionType.Percent,
                DiscountPercent = 15m
            };

            var mapper = new PromotionMapper();

            var entity = mapper.From(model);

            Assert.IsNotNull(entity);
            Assert.AreEqual(model.Id, entity.Id);
            Assert.AreEqual(model.CategoryId, entity.CategoryId);
            Assert.AreEqual(model.DiscountAmount, entity.DiscountAmount);
            Assert.AreEqual(model.SpendAmount, entity.SpendAmount);
            Assert.AreEqual(model.PromotionType, entity.PromotionType);
            Assert.AreEqual(model.DiscountPercent, entity.DiscountPercent);
        }
    }
}
