using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Mapper.Base;
using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Repository.Mapper
{
    public class ProductMapper : BaseMapper<Product, ProductDb>
    {
        private readonly IMapper<Category, CategoryDb> _categoryMapper;

        public ProductMapper(IMapperFactory mapperFactory)
        {
            _categoryMapper = mapperFactory.GetMapper<Category, CategoryDb>();
        }
        protected override void Map(Product model, ProductDb entity)
        {
            base.Map(model, entity);

            entity.CategoryId = model.CategoryId;
            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.Quantity = model.Quantity;
        }

        protected override void Map(ProductDb entity, Product model)
        {

            base.Map(entity, model);

            model.CategoryId = entity.CategoryId;
            model.Name = entity.Name;
            model.Price = entity.Price;
            model.Quantity = entity.Quantity;
            model.Category = _categoryMapper?.From(entity.Category);
        }
    }
}
