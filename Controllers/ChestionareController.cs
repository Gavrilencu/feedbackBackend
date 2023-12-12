using Microsoft.AspNetCore.Mvc;
using feedback.Data;
using feedback.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace feedback.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChestionareController : ControllerBase
    {
        private readonly FeedbackContext _context;

        public ChestionareController(FeedbackContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> CreateChestionar([FromBody] ChestionarCreateRequest request)
        {
            var chestionar = new Chestionar
            {
                Nume = request.Nume,
                CreatorId = request.Username,
                Tip = request.Tip,

            };

            _context.Chestionare.Add(chestionar);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Chestionar creat cu succes." });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditChestionar(int id, [FromBody] ChestionarEditRequest request)
        {
            var chestionar = await _context.Chestionare.FindAsync(id);
            if (chestionar == null)
            {
                return NotFound(new { Message = "Chestionarul nu a fost găsit." });
            }

            if (chestionar.CreatorId != request.Username)
            {
                return new ObjectResult(new { Message = "Nu aveți permisiunea de a modifica acest chestionar." }) { StatusCode = 403 };
            }

            chestionar.Nume = request.Nume;
            _context.Entry(chestionar).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Chestionar modificat cu succes." });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChestionar(int id, [FromBody] ChestionarDeleteRequest request)
        {
            var chestionar = await _context.Chestionare.FindAsync(id);
            if (chestionar == null)
            {
                return NotFound(new { Message = "Chestionarul nu a fost găsit." });
            }

            if (chestionar.CreatorId != request.Username)
            {
                return new ObjectResult(new { Message = "Nu aveți permisiunea de a șterge acest chestionar." }) { StatusCode = 403 };
            }

            _context.Chestionare.Remove(chestionar);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Chestionar șters cu succes." });
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chestionar>>> GetAllChestionare()
        {
            var chestionare = await _context.Chestionare.ToListAsync();
            return Ok(chestionare.Select(c => new { c.Id, c.Nume, c.CreatorId,c.Tip }));
        }
    }

    public class ChestionarCreateRequest
    {
        public string Nume { get; set; }
        public string Username { get; set; }
        public string Tip { get; set; }

    }

    public class ChestionarEditRequest
    {
        public string Nume { get; set; }
        public string Username { get; set; }
        public string Tip { get; set; }

    }


    public class ChestionarDeleteRequest
    {
        public string Username { get; set; }
    }
}
