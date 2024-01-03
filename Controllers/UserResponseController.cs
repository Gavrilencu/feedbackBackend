using feedback.Data;
using feedback.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System; // For Convert
using System.Collections.Generic; // For List

namespace feedback.Controllers
{
    [ApiController]
    [Route("/feedback/api/[controller]")]
    public class UserResponseController : ControllerBase
    {
        private readonly FeedbackContext _context;

        public UserResponseController(FeedbackContext context)
        {
            _context = context;
        }
        readonly DateTime currentDate = DateTime.Today;
        private static string EncodeUsername(string username, int idChestionar)
        {

            // Concat username and IdChestionar and currentDate and Time
            string toEncode = $"{username}{idChestionar}";

            // Base64 encode Id Chestionar and Date and time
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(toEncode);
            return Convert.ToBase64String(plainTextBytes);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserResponses([FromBody] List<UserResponseCreateRequest> requests)
        {
            foreach (var request in requests)
            {
                // Encode the username with IdChestionar
                var encodedUsername = EncodeUsername(request.Username, request.IdChestionar.Value);

                var userResponse = new UserResponse
                {
                    QuestionId = request.QuestionId,
                    IdChestionar = request.IdChestionar.Value,
                    Raspuns = request.Raspuns,
                    ForPerson = request.ForPerson,
                    FromPerson = request.FromPerson,
                    EncodedName = encodedUsername, // Save the encoded hash
                    PostDate = request.PostDate
                };

                _context.UserResponse.Add(userResponse);
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Răspunsurile au fost salvate cu succes." });
        }

        [HttpGet("{idChestionar}/{fromUser}")]
        public async Task<ActionResult<IEnumerable<object>>> GetUserResponses(int idChestionar, string fromUser)
        {
            var userResponses = await _context.UserResponse
                .Where(r => r.IdChestionar == idChestionar && r.FromPerson == fromUser)
                .Join(_context.Intrebari, // Tabelul cu care se face join
                      response => response.QuestionId, // Cheia străină din UserResponse
                      question => question.Id, // Cheia primară din Intrebari
                      (response, question) => new // Rezultatul join-ului
                      {
                          response.Id,
                          response.QuestionId,
                          response.IdChestionar,
                          response.Raspuns,
                          response.ForPerson,
                          response.FromPerson,
                          DenumireIntrebare = question.DenumireIntrebare
                      })
                .ToListAsync();

            if (!userResponses.Any())
            {
                return NotFound(new { Message = "Nu există răspunsuri de la utilizatorul specificat pentru chestionarul dat." });
            }

            return Ok(userResponses);
        }
        [HttpGet("all/{idChestionar}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllUserResponses(int idChestionar)
        {
            var userResponses = await _context.UserResponse
                .Where(r => r.IdChestionar == idChestionar)
                .Join(_context.Intrebari, // Tabelul cu care se face join
                      response => response.QuestionId, // Cheia străină din UserResponse
                      question => question.Id, // Cheia primară din Intrebari
                      (response, question) => new // Rezultatul join-ului
                      {
                          response.Id,
                          response.QuestionId,
                          response.IdChestionar,
                          response.Raspuns,
                          response.ForPerson,
                          response.FromPerson,
                          DenumireIntrebare = question.DenumireIntrebare
                      })
                .ToListAsync();

            if (!userResponses.Any())
            {
                return NotFound(new { Message = "Nu există răspunsuri pentru chestionarul specificat." });
            }

            return Ok(userResponses);
        }
        [HttpGet("responses-for-all/{idChestionar}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllResponsesForAllUsers(int idChestionar)
        {
            var responsesForAll = await _context.UserResponse
                .Where(r => r.IdChestionar == idChestionar)
                .Join(_context.Intrebari, // Join cu tabelul Intrebari
                      response => response.QuestionId, // Cheia străină din UserResponse
                      question => question.Id, // Cheia primară din Intrebari
                      (response, question) => new // Rezultatul join-ului
                      {
                          response.ForPerson,
                          Intrebare = question.DenumireIntrebare,
                          response.Raspuns
                      })
                .GroupBy(r => r.ForPerson) // Grupăm răspunsurile pe baza lui ForPerson
                .Select(group => new
                {
                    ForPerson = group.Key,
                    IntrebariSiRaspunsuri = group.Select(item => new
                    {
                        item.Intrebare,
                        item.Raspuns
                    }).ToList()
                })
                .ToListAsync();

            if (!responsesForAll.Any())
            {
                return NotFound(new { Message = "Nu există răspunsuri pentru chestionarul specificat." });
            }

            return Ok(responsesForAll);
        }

    }

    public class UserResponseCreateRequest
    {
        public int? QuestionId { get; set; }
        public int? IdChestionar { get; set; }
        public string? Raspuns { get; set; }
        public string? ForPerson { get; set; }
        public string? FromPerson { get; set; }
        public string? Username { get; set; } // Added this line
        public string? PostDate { get; set; }

    }

    public class UserResponseEditRequest
    {
        public int? QuestionId { get; set; }
        public int? IdChestionar { get; set; }
        public string? Raspuns { get; set; }
        public string? ForPerson { get; set; }
        public string? FromPerson { get; set; }
    }
}
