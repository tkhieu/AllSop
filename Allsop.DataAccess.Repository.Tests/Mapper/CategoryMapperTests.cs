using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Mapper;
using Allsop.Service.Contract.Model;
using NUnit.Framework;

namespace Allsop.DataAccess.Repository.Tests.Mapper
{
    [TestFixture]
    public class CategoryMapperTests
    {
        [Test]
        public void Should_Be_Injected()
        {
            // Arrange
            var serviceProvider = DependencyInjector.GetServiceProvider();
            var mapperFactory = serviceProvider.GetService(typeof(IMapperFactory)) as IMapperFactory;

            // Act
            var mapper = mapperFactory.GetMapper<Category, CategoryDb>() as CategoryMapper;

            // Assert
            Assert.IsNotNull(mapper);
        }

        [Test]
        public void CategoryMapper_MapEntityToModel_ShouldMapCorrectly()
        {
            var entity = new CategoryDb()
            {
                Id = Guid.NewGuid(),
                Name = "Category 1"
            };

            var mapper = new CategoryMapper();

            var model = mapper.From(entity);

            Assert.IsNotNull(model);
            Assert.AreEqual(entity.Id, model.Id);
            Assert.AreEqual(entity.Name, model.Name);
        }

        [Test]
        public void CategoryMapper_MapModelToEntity_ShouldMapCorrectly()
        {
            var model = new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category 1"
            };

            var mapper = new CategoryMapper();

            var entity = mapper.From(model);

            Assert.IsNotNull(entity);
            Assert.AreEqual(model.Id, entity.Id);
            Assert.AreEqual(model.Name, entity.Name);
        }
    }
}