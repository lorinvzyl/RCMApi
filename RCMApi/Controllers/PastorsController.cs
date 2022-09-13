using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCMApi.Models;
using RCMAppApi.Models;

namespace RCMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PastorsController : ControllerBase
    {
        private readonly DataContext _context;

        public PastorsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Pastors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pastor>>> GetPastor()
        {
          if (_context.Pastor == null)
          {
              return NotFound();
          }
            return await _context.Pastor.ToListAsync();
        }

        // GET: api/Pastors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pastor>> GetPastor(int id)
        {
          if (_context.Pastor == null)
          {
              return NotFound();
          }
            var pastor = await _context.Pastor.FindAsync(id);

            if (pastor == null)
            {
                return NotFound();
            }

            return pastor;
        }

        // PUT: api/Pastors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPastor(int id, Pastor pastor)
        {
            if (id != pastor.Id)
            {
                return BadRequest();
            }

            _context.Entry(pastor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PastorExists(id))
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

        // POST: api/Pastors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pastor>> PostPastor([FromBody] Pastor pastor)
        {
          if (_context.Pastor == null)
          {
              return Problem("Entity set 'DataContext.Pastor'  is null.");
          }
            _context.Pastor.Add(pastor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPastor", new { id = pastor.Id }, pastor);
        }

        // DELETE: api/Pastors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePastor(int id)
        {
            if (_context.Pastor == null)
            {
                return NotFound();
            }
            var pastor = await _context.Pastor.FindAsync(id);
            if (pastor == null)
            {
                return NotFound();
            }

            _context.Pastor.Remove(pastor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PastorExists(int id)
        {
            return (_context.Pastor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
