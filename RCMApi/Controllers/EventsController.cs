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
    public class EventsController : ControllerBase
    {
        private readonly DataContext _context;

        public EventsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetEvents()
        {
            if (_context.Event == null)
            {
                return NotFound();
            }
            return await _context.Event.Select(x => EventDTO(x)).ToListAsync();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            if (_context.Event == null)
            {
                return NotFound();
            }
            var @event = await _context.Event.FindAsync(id);

            if (@event == null)
            {
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, [FromBody] EventDTO eventDTO)
        {
            if (id != eventDTO.Id)
            {
                return BadRequest();
            }

            Event _event = new()
            {
                Id = (int)eventDTO.Id,
                EventDate = eventDTO.EventDate,
                RSVPCloseDate = eventDTO.RSVPCloseDate,
                SpacesAvailable = eventDTO.SpacesAvailable,
                SpacesTaken = eventDTO.SpacesTaken,
                EventName = eventDTO.EventName,
                Venue = eventDTO.Venue,
                EventDescription = eventDTO.EventDescription,
            };

            _context.Entry(_event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
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

        // POST: api/Events
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(EventDTO @event)
        {
            if (_context.Event == null)
            {
                return Problem("Entity set 'DataContext.Events'  is null.");
            }

            Event _event = new()
            {
                EventDate = @event.EventDate,
                EventDescription = @event.EventDescription,
                EventName = @event.EventName,
                RSVPCloseDate = @event.RSVPCloseDate,
                SpacesAvailable = @event.SpacesAvailable,
                SpacesTaken = @event.SpacesTaken,
                Venue = @event.Venue
            };

            _context.Event.Add(_event);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvent", new { id = _event.Id }, _event);
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (_context.Event == null)
            {
                return NotFound();
            }
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return (_context.Event?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static EventDTO EventDTO(Event _event) =>
            new()
            {
                Id = _event.Id,
                EventDate = _event.EventDate,
                EventDescription = _event.EventDescription,
                EventName = _event.EventName,
                RSVPCloseDate = _event.RSVPCloseDate,
                SpacesAvailable = _event.SpacesAvailable,
                SpacesTaken = _event.SpacesTaken,
                Venue = _event.Venue
            };
    }
}
