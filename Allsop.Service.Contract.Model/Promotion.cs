using Allsop.Common.Enums;
using Allsop.Service.Contract.Model.Base;

namespace Allsop.Service.Contract.Model
{
    public class Promotion : BaseModel
    {
        public string Voucher { get; set; }
        public decimal DiscountPercent { get; set; }
        public int SpendQuantity { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal SpendAmount { get; set; }
        public PromotionType PromotionType { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
