using Allsop.DataAccess.Contract.Mapper;
using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Mapper.Base;
using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Repository.Mapper
{
    public class ShoppingCartMapper : BaseMapper<ShoppingCart, ShoppingCartDb>
    {
        private readonly IMapper<ShoppingCartItem, ShoppingCartItemDb> _shoppingCartItemMapper;

        public ShoppingCartMapper(IMapperFactory mapperFactory)
        {
            _shoppingCartItemMapper = mapperFactory.GetMapper<ShoppingCartItem, ShoppingCartItemDb>();
        }
        protected override void Map(ShoppingCart model, ShoppingCartDb entity)
        {
            base.Map(model, entity);

            entity.UserId = model.UserId;
            entity.Voucher = model.Voucher;
            entity.DiscountAmount = model.DiscountAmount;
            entity.ShoppingCartItems = model.ShoppingCartItems.Select(_shoppingCartItemMapper.From).ToList();
        }

        protected override void Map(ShoppingCartDb entity, ShoppingCart model)
        {
            base.Map(entity, model);

            model.UserId = entity.UserId;
            model.Voucher = entity.Voucher;
            model.DiscountAmount = entity.DiscountAmount;
            model.ShoppingCartItems = entity.ShoppingCartItems.Select(_shoppingCartItemMapper.From).ToList();
        }
    }
}
