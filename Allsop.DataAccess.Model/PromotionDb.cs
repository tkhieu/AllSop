using Allsop.Common.Enums;
using Allsop.DataAccess.Model.Base;

namespace Allsop.DataAccess.Model
{
    public class PromotionDb: BaseEntityDb
    {
        public string? Voucher { get; set; }
        public decimal DiscountPercent { get; set; }
        public int SpendQuantity { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SpendAmount { get; set; }
        public PromotionType PromotionType { get; set; }
        public Guid? CategoryId { get; set; }
        public CategoryDb Category { get; set; }
    }
}
