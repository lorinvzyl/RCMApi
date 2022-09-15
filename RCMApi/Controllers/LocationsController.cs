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
    public class LocationsController : ControllerBase
    {
        private readonly DataContext _context;

        public LocationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Locations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
        {
            if (_context.Location == null)
                return Problem("DataContext Location is null.");

            var locations = await _context.Location.ToListAsync();

            IEnumerable<LocationDTO> result = new List<LocationDTO>();

            foreach(var location in locations)
            {
                var pastor = await _context.Pastor.FindAsync(location.PastorId);
                var pastorName = await _context.User.FindAsync(pastor.UserId);

                result = result.Append(new LocationDTO
                {
                    Id = location.Id,
                    MapsURL = location.MapsURL,
                    Name = location.Name,
                    Pastor = pastorName.Name
                });
            }

            return CreatedAtAction("GetLocations", result);
        }

        // GET: api/Locations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Location>> GetLocation(int id)
        {
          if (_context.Location == null)
          {
              return NotFound();
          }
            var location = await _context.Location.FindAsync(id);

            if (location == null)
            {
                return NotFound();
            }

            return location;
        }

        // PUT: api/Locations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocation(int id, Location location)
        {
            if (id != location.Id)
            {
                return BadRequest();
            }

            _context.Entry(location).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationExists(id))
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

        // POST: api/Locations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Location>> PostLocation(Location location)
        {
          if (_context.Location == null)
          {
              return Problem("Entity set 'DataContext.Location'  is null.");
          }
            _context.Location.Add(location);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocation", new { id = location.Id }, location);
        }

        // DELETE: api/Locations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            if (_context.Location == null)
            {
                return NotFound();
            }
            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }

            _context.Location.Remove(location);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LocationExists(int id)
        {
            return (_context.Location?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static LocationDTO LocationDTO(Location location) =>
            new()
            {
                Id = location.Id,
                Name = location.Name,
                MapsURL = location.MapsURL
            };
    }
}
