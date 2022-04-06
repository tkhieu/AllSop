using Allsop.DataAccess.Model;
using Allsop.DataAccess.Repository.Mapper.Base;
using Allsop.Service.Contract.Model;

namespace Allsop.DataAccess.Repository.Mapper
{
    public class PromotionMapper : BaseMapper<Promotion, PromotionDb>
    {
        protected override void Map(Promotion model, PromotionDb entity)
        {
            base.Map(model, entity);

            entity.Voucher = model.Voucher;
            entity.CategoryId = model.CategoryId;
            entity.DiscountPercent = model.DiscountPercent;
            entity.DiscountAmount = model.DiscountAmount;
            entity.PromotionType = model.PromotionType;
            entity.SpendAmount = model.SpendAmount;
            entity.SpendQuantity = model.SpendQuantity;
        }

        protected override void Map(PromotionDb entity, Promotion model)
        {
            base.Map(entity, model);

            model.Voucher = entity.Voucher;
            model.CategoryId = entity.CategoryId;
            model.DiscountPercent = entity.DiscountPercent;
            model.DiscountAmount = entity.DiscountAmount;
            model.PromotionType = entity.PromotionType;
            model.SpendAmount = entity.SpendAmount;
            model.SpendQuantity = entity.SpendQuantity;
        }
    }
}
