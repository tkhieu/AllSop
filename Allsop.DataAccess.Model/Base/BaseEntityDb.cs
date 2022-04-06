using System.ComponentModel.DataAnnotations;

namespace Allsop.DataAccess.Model.Base
{
    public abstract class BaseEntityDb
    {
        [Key]
        public Guid Id { get; set; }
    }
}
