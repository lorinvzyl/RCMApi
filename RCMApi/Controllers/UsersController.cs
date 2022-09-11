using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RCMApi.Models;
using RCMAppApi.Models;
using RCMAppApi.Services;

namespace RCMAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            return await _context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
          if (_context.User == null)
          {
              return NotFound();
          }
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost("email={email}&password={password}")]
        public async Task<ActionResult> LoginUser(string email, string password)
        {
            if (_context.User == null)
                return NotFound();
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return NotFound();

            byte[] passwordIn = Encoding.UTF8.GetBytes(password);
            byte[] hashedPassword = user.HashedPassword;

            HashingService hashingService = new(passwordIn, hashedPassword);
            bool login = hashingService.VerifyHash();

            return CreatedAtAction("LoginUser", login);
        }

        [HttpGet("email={email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            if (_context.User == null)
                return NotFound();

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return NotFound();

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] UserString userString)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'DataContext.Users'  is null.");
            }
            if (userString.HashedPassword == null)
                return Problem("No password entered");

            User user = new()
            {
                Email = userString.Email,
                Name = userString.Name,
                Surname = userString.Surname,
                DateOfBirth = userString.DateOfBirth,
                HashedPassword = Encoding.UTF8.GetBytes(userString.HashedPassword)
            };

            user.IsNewsletter = false;
            user.IsDeleted = false;
            user.IsActive = true;

            HashingService hashingService = new(user.HashedPassword);
            hashingService.HashPassword();
            user.HashedPassword = hashingService.Hash.ToArray();
            user.Iterations = hashingService.iterations;
            user.MemoryLimit = hashingService.memorySize;

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static UserDTO UserDTO(User user) =>
            new()
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                Name = user.Name,
                IsNewsletter = user.IsNewsletter,
                Surname = user.Surname,
                HashedPassword = user.HashedPassword,
                MemoryLimit = user.MemoryLimit,
                Iterations = user.Iterations
            };
    }
}
