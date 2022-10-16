using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCMApi.Models;
using RCMAppApi.Models;

namespace RCMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly DataContext _context;

        public RepliesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Replies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reply>>> GetReply()
        {
          if (_context.Reply == null)
          {
              return NotFound();
          }
            return await _context.Reply.ToListAsync();
        }

        // GET: api/Replies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ReplyDTO>>> GetReply(int id)
        {
          if (_context.Reply == null)
          {
              return NotFound();
          }
            if (_context.User == null)
                return NotFound();

            var reply = await _context.Reply.Include(z => z.User).Where(x => x.CommentId == id).Select(r => new ReplyDTO
            {
                Id = r.Id,
                CommentId = r.CommentId,
                UserName = r.User.Name,
                CommentText = r.CommentText
            }).ToListAsync();

            if (reply == null)
            {
                return NotFound();
            }

            return reply;
        }

        // PUT: api/Replies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReply(int id, Reply reply)
        {
            if (id != reply.Id)
            {
                return BadRequest();
            }

            _context.Entry(reply).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplyExists(id))
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

        // POST: api/Replies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reply>> PostReply(ReplyDTO replyDTO)
        {
          if (_context.Reply == null)
          {
              return Problem("Entity set 'DataContext.Reply'  is null.");
          }

            if (_context.User == null)
                return NotFound();

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == replyDTO.UserEmail);

            if (user == null)
                return NotFound();

            Reply reply = new()
            {
                CommentId = (int)replyDTO.CommentId,
                CommentText = replyDTO.CommentText,
                UserId = user.Id
            };

            _context.Reply.Add(reply);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReply", new { id = reply.Id }, reply);
        }

        // DELETE: api/Replies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReply(int id)
        {
            if (_context.Reply == null)
            {
                return NotFound();
            }
            var reply = await _context.Reply.FindAsync(id);
            if (reply == null)
            {
                return NotFound();
            }

            _context.Reply.Remove(reply);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReplyExists(int id)
        {
            return (_context.Reply?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
