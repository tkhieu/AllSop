namespace Allsop.Service.Model.Base
{
    public class BaseModel: InternalBaseModel
    {
        public Guid Id { get; set; }
    }

    public abstract class InternalBaseModel
    {
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public string Owner { get; set; }

        public DateTime ModifiedAt { get; set; }

        public string ModifiedBy { get; set; }

        public string Tenant { get; set; }

        public bool IsDisabled { get; set; }

        public HashSet<string> ChangedProperties { private set; get; } = new HashSet<string>();

        public void SetChangedProperties(IEnumerable<string> names)
        {
            ChangedProperties = new HashSet<string>(names);
        }

    }
}
