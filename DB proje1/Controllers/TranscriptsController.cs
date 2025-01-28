using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB_proje1.Models;

namespace DB_proje1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranscriptsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TranscriptsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transcript>>> GetTranscripts()
        {
            return await _context.Transcripts.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Transcript>> GetTranscript(int id)
        {
            var transcript = await _context.Transcripts.FindAsync(id);

            if (transcript == null)
            {
                return NotFound();
            }

            return transcript;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTranscript(int id, Transcript transcript)
        {
            if (id != transcript.TranscriptID)
            {
                return BadRequest();
            }

            _context.Entry(transcript).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TranscriptExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<Transcript>> PostTranscript(Transcript transcript)
        {
            _context.Transcripts.Add(transcript);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTranscript", new { id = transcript.TranscriptID }, transcript);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTranscript(int id)
        {
            var transcript = await _context.Transcripts.FindAsync(id);
            if (transcript == null)
            {
                return NotFound();
            }

            _context.Transcripts.Remove(transcript);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TranscriptExists(int id)
        {
            return _context.Transcripts.Any(e => e.TranscriptID == id);
        }
    }
}
