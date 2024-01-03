namespace feedback.Models
{
    public class UserResponse
    {
        public int? Id { get; set; }
        public int? QuestionId { get; set; }
        public int? IdChestionar { get; set; } // ID-ul chestionarului
        public string? Raspuns { get; set; }
        public string? ForPerson { get; set; } // Opțional, folosit doar pentru răspunsurile de tip feedback
        public string? FromPerson { get; set; }
        public string? EncodedName { get; set; }
        public string? PostDate { get; set; }

    }


}
