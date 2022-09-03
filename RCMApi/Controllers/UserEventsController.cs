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
    public class UserEventsController : ControllerBase
    {
        private readonly DataContext _context;

        public UserEventsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/UserEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEvent>>> GetUserEvents()
        {
          if (_context.UserEvents == null)
          {
              return NotFound();
          }
            return await _context.UserEvents.ToListAsync();
        }

        // GET: api/UserEvents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEvent>> GetUserEvent(int id)
        {
          if (_context.UserEvents == null)
          {
              return NotFound();
          }
            var userEvent = await _context.UserEvents.FindAsync(id);

            if (userEvent == null)
            {
                return NotFound();
            }

            return userEvent;
        }

        // PUT: api/UserEvents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserEvent(int id, UserEvent userEvent)
        {
            if (id != userEvent.Id)
            {
                return BadRequest();
            }

            _context.Entry(userEvent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserEventExists(id))
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

        // POST: api/UserEvents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserEvent>> PostUserEvent(UserEvent userEvent)
        {
          if (_context.UserEvents == null)
          {
              return Problem("Entity set 'DataContext.UserEvents'  is null.");
          }
            _context.UserEvents.Add(userEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserEvent", new { id = userEvent.Id }, userEvent);
        }

        // DELETE: api/UserEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserEvent(int id)
        {
            if (_context.UserEvents == null)
            {
                return NotFound();
            }
            var userEvent = await _context.UserEvents.FindAsync(id);
            if (userEvent == null)
            {
                return NotFound();
            }

            _context.UserEvents.Remove(userEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserEventExists(int id)
        {
            return (_context.UserEvents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
