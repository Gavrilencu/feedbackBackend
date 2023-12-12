namespace feedback.Models
{
    public class Chestionar
    {
        public int? Id { get; set; }
        public string? Nume { get; set; }
        public string? CreatorId { get; set; }
        public string? Tip { get; set; } // "simplu" sau "feedback"
    }

}
