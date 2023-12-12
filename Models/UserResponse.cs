namespace feedback.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int IdChestionar { get; set; } // ID-ul chestionarului
        public string Raspuns { get; set; }
        public string NumePersoana { get; set; } // Opțional, folosit doar pentru răspunsurile de tip feedback
    }


}
