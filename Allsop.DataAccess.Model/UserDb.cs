using Allsop.DataAccess.Model.Base;

namespace Allsop.DataAccess.Model
{
    public class UserDb: BaseEntityDb
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
