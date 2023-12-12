using feedback.Data;
using feedback.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace feedback.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserResponseController : ControllerBase
    {
        private readonly FeedbackContext _context;

        public UserResponseController(FeedbackContext context)
        {
            _context = context;
        }

        public string DenumireIntrebare { get; internal set; }

        // POST: api/UserResponse
        [HttpPost]
        public async Task<ActionResult> CreateUserResponses([FromBody] List<UserResponseCreateRequest> requests)
        {
            foreach (var request in requests)
            {
                var userResponse = new UserResponse
                {
                    QuestionId = request.QuestionId,
                    IdChestionar = request.IdChestionar,
                    Raspuns = request.Raspuns,
                    NumePersoana = request.NumePersoana
                };

                _context.UserResponse.Add(userResponse);
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Răspunsurile au fost salvate cu succes." });
        }

        // PUT: api/UserResponse/5

        // DELETE: api/UserRespons

        // GET: api/UserResponse
        [HttpGet("{idChestionar}")]
        public async Task<ActionResult<IEnumerable<object>>> GetUserResponses(int idChestionar)
        {
            var userResponses = await _context.UserResponse
                .Where(r => r.IdChestionar == idChestionar)
                .Join(_context.Intrebari, // Tabelul cu care se face join
                      response => response.QuestionId, // Cheia străină din UserResponse
                      question => question.Id, // Cheia primară din Intrebare
                      (response, question) => new // Rezultatul join-ului
                      {
                          response.Id,
                          response.QuestionId,
                          response.IdChestionar,
                          response.Raspuns,
                          response.NumePersoana,
                          DenumireIntrebare = question.DenumireIntrebare
                      })
                .ToListAsync();

            if (!userResponses.Any())
            {
                return NotFound(new { Message = "Nu există răspunsuri pentru chestionarul specificat." });
            }

            return Ok(userResponses);
        }

    }

    public class UserResponseCreateRequest
    {
        public int QuestionId { get; set; }
        public int IdChestionar { get; set; }
        public string Raspuns { get; set; }
        public string NumePersoana { get; set; }
    }

    public class UserResponseEditRequest
    {
        public int QuestionId { get; set; }
        public int IdChestionar { get; set; }
        public string Raspuns { get; set; }
        public string NumePersoana { get; set; }
    }
}
