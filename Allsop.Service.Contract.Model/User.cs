using Allsop.Service.Contract.Model.Base;

namespace Allsop.Service.Contract.Model
{
    public class User:BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
