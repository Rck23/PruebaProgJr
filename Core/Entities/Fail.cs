
namespace Core.Entities
{
    public class Fail: BaseEntity
    {
        public string Description { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool IsResolved { get; set; }
        public string AffectedEquipment { get; set; }
        public string Responsible { get; set; }
        public string Comments { get; set; }
    }
}
