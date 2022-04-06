using System.ComponentModel.DataAnnotations;
using Allsop.DataAccess.Model.Base;

namespace Allsop.DataAccess.Model
{
    public class CategoryDb: BaseEntityDb
    {
        [MaxLength(256)]
        public string Name { get; set; }
    }
}