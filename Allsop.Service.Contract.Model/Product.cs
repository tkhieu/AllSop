using Allsop.Service.Contract.Model.Base;

namespace Allsop.Service.Contract.Model
{
    public class Product: BaseModel
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}