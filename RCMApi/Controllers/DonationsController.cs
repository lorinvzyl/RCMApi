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
    public class DonationsController : ControllerBase
    {
        private readonly DataContext _context;

        public DonationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Donations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donation>>> GetDonations()
        {
          if (_context.Donations == null)
          {
              return NotFound();
          }
            return await _context.Donations.ToListAsync();
        }

        // GET: api/Donations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Donation>> GetDonation(int id)
        {
          if (_context.Donations == null)
          {
              return NotFound();
          }
            var donation = await _context.Donations.FindAsync(id);

            if (donation == null)
            {
                return NotFound();
            }

            return donation;
        }

        // PUT: api/Donations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDonation(int id, Donation donation)
        {
            if (id != donation.Id)
            {
                return BadRequest();
            }

            _context.Entry(donation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationExists(id))
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

        // POST: api/Donations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Donation>> PostDonation(Donation donation)
        {
          if (_context.Donations == null)
          {
              return Problem("Entity set 'DataContext.Donations'  is null.");
          }
            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDonation", new { id = donation.Id }, donation);
        }

        // DELETE: api/Donations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonation(int id)
        {
            if (_context.Donations == null)
            {
                return NotFound();
            }
            var donation = await _context.Donations.FindAsync(id);
            if (donation == null)
            {
                return NotFound();
            }

            _context.Donations.Remove(donation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DonationExists(int id)
        {
            return (_context.Donations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
