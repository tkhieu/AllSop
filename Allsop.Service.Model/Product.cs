using Allsop.Service.Model.Base;

namespace Allsop.Service.Model
{
    public class Product: BaseModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}