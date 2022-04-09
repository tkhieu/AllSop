using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Mapper.Base;
using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Repository.Mapper
{
    public class ShoppingCartItemMapper : BaseMapper<ShoppingCartItem, ShoppingCartItemDb>
    {
        private readonly IMapper<Product, ProductDb> _productMapper;
        public ShoppingCartItemMapper(IMapperFactory mapperFactory)
        {
            _productMapper = mapperFactory.GetMapper<Product,ProductDb>();
        }

        protected override void Map(ShoppingCartItem model, ShoppingCartItemDb entity)
        {
            base.Map(model, entity);

            entity.Quantity = model.Quantity;
            entity.ProductId = model.ProductId;
            entity.ShoppingCartId = model.ShoppingCartId;
        }

        protected override void Map(ShoppingCartItemDb entity, ShoppingCartItem model)
        {
            base.Map(entity, model);

            model.Quantity = entity.Quantity;
            model.ProductId = entity.ProductId;
            model.ShoppingCartId = entity.ShoppingCartId;
        }
    }
}
