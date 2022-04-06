using Allsop.Service.Contract.Model.Base;

namespace Allsop.Service.Contract.Model
{
    public class Category: BaseModel
    {
        public Category()
        {
            
        }

        public Category(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
