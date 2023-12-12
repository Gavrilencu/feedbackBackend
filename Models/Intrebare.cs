namespace feedback.Models
{
    public class Intrebare
    {
        public int Id { get; set; }
        public string DenumireIntrebare { get; set; }
        public int IdChestionar { get; set; } // Foreign key pentru Chestionar
    }

}
