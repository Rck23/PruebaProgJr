namespace API.Dtos
{
    public class FailDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateRegistered { get; set; }
        public bool IsResolved { get; set; }
        public string AffectedEquipment { get; set; }
        public string Responsible { get; set; }
        public string Comments { get; set; }
    }
}
