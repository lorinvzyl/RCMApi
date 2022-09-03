using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCMAppApi.Models;

namespace RCMAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewslettersController : ControllerBase
    {
        private readonly DataContext _context;

        public NewslettersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Newsletters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Newsletter>>> GetNewsletter()
        {
          if (_context.Newsletter == null)
          {
              return NotFound();
          }
            return await _context.Newsletter.ToListAsync();
        }

        // GET: api/Newsletters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Newsletter>> GetNewsletter(int id)
        {
          if (_context.Newsletter == null)
          {
              return NotFound();
          }
            var newsletter = await _context.Newsletter.FindAsync(id);

            if (newsletter == null)
            {
                return NotFound();
            }

            return newsletter;
        }

        // PUT: api/Newsletters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsletter(int id, Newsletter newsletter)
        {
            if (id != newsletter.Id)
            {
                return BadRequest();
            }

            _context.Entry(newsletter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsletterExists(id))
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

        // POST: api/Newsletters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Newsletter>> PostNewsletter(Newsletter newsletter)
        {
          if (_context.Newsletter == null)
          {
              return Problem("Entity set 'DataContext.Newsletter'  is null.");
          }
            _context.Newsletter.Add(newsletter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsletter", new { id = newsletter.Id }, newsletter);
        }

        // DELETE: api/Newsletters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNewsletter(int id)
        {
            if (_context.Newsletter == null)
            {
                return NotFound();
            }
            var newsletter = await _context.Newsletter.FindAsync(id);
            if (newsletter == null)
            {
                return NotFound();
            }

            _context.Newsletter.Remove(newsletter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NewsletterExists(int id)
        {
            return (_context.Newsletter?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
