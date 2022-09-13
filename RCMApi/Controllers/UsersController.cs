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

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] UserDTO user)
        {
            if (_context.User == null)
                return NotFound();
            var userAcc = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);

            if (userAcc == null)
                return NotFound();

            if (user.Password == null)
                return NotFound();

            byte[] passwordIn = Encoding.UTF8.GetBytes(user.Password);

            if (userAcc.HashedPassword == null)
                return NotFound();

            byte[] hashedPassword = userAcc.HashedPassword;

            HashingService hashingService = new(passwordIn, hashedPassword);
            bool login = hashingService.VerifyHash();

            return CreatedAtAction("LoginUser", login);
        }

        //GET: api/Users/email
        [HttpGet("email")]
        public async Task<ActionResult<User>> GetUserByEmail([FromBody] string email)
        {
            if (_context.User == null)
                return NotFound();

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
                return NotFound();

            return user;
        }

        // PUT: api/Users/email={email}
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("email={email}")]
        public async Task<IActionResult> PutUser(string email, [FromBody] UserDTO userDTO)
        {
            if (email != userDTO.Email)
            {
                return BadRequest();
            }

            if (_context.User == null)
                return NotFound();

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == userDTO.Email);

            if (user == null)
                return NotFound();

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
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
        public async Task<ActionResult<User>> PostUser([FromBody] UserDTO userDTO)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'DataContext.Users'  is null.");
            }
            if (userDTO.Password == null)
                return Problem("No password entered");

            User user = new()
            {
                Email = userDTO.Email,
                Name = userDTO.Name,
                Surname = userDTO.Surname,
                DateOfBirth = userDTO.DateOfBirth,
                HashedPassword = Encoding.UTF8.GetBytes(userDTO.Password)
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

        // DELETE: api/Users/email={email}
        [HttpDelete("email={email}")]
        public async Task<IActionResult> DeleteUser(string email)
        {
            if (_context.User == null)
            {
                return NotFound();
            }
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);

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
                Password = Encoding.UTF8.GetString(user.HashedPassword)
            };
    }
}
