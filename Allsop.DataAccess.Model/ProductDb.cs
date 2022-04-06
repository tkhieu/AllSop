using System.ComponentModel.DataAnnotations;
using Allsop.DataAccess.Model.Base;

namespace Allsop.DataAccess.Model
{
    public class ProductDb: BaseEntityDb
    {
        [MaxLength(256)]
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDb Category { get; set; }
    }
}
