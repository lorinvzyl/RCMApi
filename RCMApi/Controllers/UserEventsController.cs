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
        public async Task<ActionResult<IEnumerable<UserEventDTO>>> GetUserEvents()
        {
            if (_context.UserEvent == null)
            {
                return NotFound();
            }

            var userEvents = await _context.UserEvent.ToListAsync();

            IEnumerable<UserEventDTO> result = new List<UserEventDTO>();

            foreach(var userEvent in userEvents)
            {
                if (_context.User == null)
                    return NotFound();

                var user = await _context.User.FindAsync(userEvent.UserId);

                if(user == null || user.Email == null)
                    return NotFound();

                result = result.Append(new UserEventDTO
                {
                    IsAttended = userEvent.IsAttended,
                    EventId = userEvent.EventId,
                    UserEmail = user.Email
                });
            }

            return CreatedAtAction("GetUserEvents", result);
        }

        // GET: api/UserEvents/5 -> uses UserId
        [HttpGet("{id}")]
        public async Task<ActionResult<UserEventDTO>> GetUserEvent(int id)
        {
          if (_context.UserEvent == null || _context.User == null)
          {
              return NotFound();
          }
            var userEvent = await _context.UserEvent.FirstOrDefaultAsync(u => u.UserId == id);
            var user = await _context.User.FindAsync(userEvent.UserId);

            var _userEvent = new UserEventDTO()
            {
                UserEmail = user.Email,
                EventId = userEvent.EventId,
                IsAttended = userEvent.IsAttended
            };

            if (userEvent == null)
            {
                return NotFound();
            }

            return _userEvent;
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
        public async Task<ActionResult<UserEvent>> PostUserEvent(UserEventDTO userEventDTO)
        {
            if (_context.UserEvent == null)
            {
                return Problem("Entity set 'DataContext.UserEvents'  is null.");
            }

            if (_context.User == null)
                return NotFound();

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == userEventDTO.UserEmail);

            if (user == null)
                return NotFound();

            UserEvent userEvent = new()
            {
                EventId = userEventDTO.EventId,
                IsAttended = userEventDTO.IsAttended,
                UserId = user.Id
            };

            _context.UserEvent.Add(userEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserEvent", new { id = userEvent.Id }, userEvent);
        }

        // POST: api/UserEvents/Attend
        [HttpPost("Attend")]
        public async Task<ActionResult> UserAttendance(UserEventDTO userEventDTO)
        {
            if (_context.UserEvent == null)
                return Problem("Entity set 'DataContext.UserEvents'  is null.");

            if (_context.User == null)
                return Problem("Entity set 'DataContext.User'  is null.");

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == userEventDTO.UserEmail);
            if (user == null)
                return NotFound();

            UserEvent userEvent = new()
            {
                UserId = user.Id,
                EventId = userEventDTO.EventId,
                IsAttended = userEventDTO.IsAttended
            };

            _context.UserEvent.Add(userEvent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Attend", new { id = userEvent.Id}, userEvent);
        }

        // DELETE: api/UserEvents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserEvent(int id)
        {
            if (_context.UserEvent == null)
            {
                return NotFound();
            }
            var userEvent = await _context.UserEvent.FindAsync(id);
            if (userEvent == null)
            {
                return NotFound();
            }

            _context.UserEvent.Remove(userEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserEventExists(int id)
        {
            return (_context.UserEvent?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static UserEventDTO UserEventDTO(UserEvent userEvent) =>
            new()
            {
                IsAttended = userEvent.IsAttended
            };
    }
}
