using feedback.Data;
using feedback.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class IntrebariController : ControllerBase
{
    private readonly FeedbackContext _context;

    public IntrebariController(FeedbackContext context)
    {
        _context = context;
    }

    // POST: api/Intrebari
    [HttpPost]
    public async Task<ActionResult> CreateIntrebare([FromBody] IntrebareCreateRequest request)
    {
        var intrebare = new Intrebare
        {
            DenumireIntrebare = request.DenumireIntrebare,
            IdChestionar = request.IdChestionar
        };

        _context.Intrebari.Add(intrebare);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Întrebare creată cu succes." });
    }


    // PUT: api/Intrebari/5
    [HttpPut("{id}")]
    public async Task<ActionResult> EditIntrebare(int id, [FromBody] IntrebareEditRequest request)
    {
        var intrebare = await _context.Intrebari.FindAsync(id);
        if (intrebare == null)
        {
            return NotFound(new { Message = "Întrebarea nu a fost găsită." });
        }

        intrebare.DenumireIntrebare = request.DenumireIntrebare;
        _context.Entry(intrebare).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Întrebare modificată cu succes." });
    }


    // DELETE: api/Intrebari/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteIntrebare(int id)
    {
        var intrebare = await _context.Intrebari.FindAsync(id);
        if (intrebare == null)
        {
            return NotFound(new { Message = "Întrebarea nu a fost găsită." });
        }

        _context.Intrebari.Remove(intrebare);
        await _context.SaveChangesAsync();

        return Ok(new { Message = "Întrebare ștearsă cu succes." });
    }


    // GET: api/Intrebari
  [HttpGet("{idChestionar}")]
public async Task<ActionResult<IEnumerable<Intrebare>>> GetAllIntrebari(int idChestionar)
{
    var intrebari = await _context.Intrebari
                        .Where(i => i.IdChestionar == idChestionar)
                        .ToListAsync();

    if (!intrebari.Any())
    {
        return NotFound(new { Message = "Nu există întrebări pentru chestionarul specificat." });
    }

    return Ok(intrebari);
}


}

public class IntrebareCreateRequest
{
    public required string DenumireIntrebare { get; set; }
    public int IdChestionar { get; set; }
}
