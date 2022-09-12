using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCMAppApi.Models;

namespace RCMApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly DataContext _context;

        public CommentsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComments()
        {
            if (_context.Comment == null)
            {
                return NotFound();
            }

            return await _context.Comment.ToListAsync();
        }

        [HttpGet("blogId={blogId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetBlogComments(int blogId)
        {
            if(_context.Comment == null)
                return NotFound();

            if (_context.User == null)
                return NotFound();

            var comments = await _context.Comment.ToListAsync();

            IEnumerable<CommentDTO> result = new List<CommentDTO>();

            foreach (var comment in comments)
            {
                if(blogId == comment.BlogId)
                {
                    int userId = (int)comment.UserId;
                    var user = await _context.User.FindAsync(userId);

                    result.Append(new CommentDTO
                    {
                        CommentId = comment.CommentId,
                        CommentText = comment.CommentText,
                        BlogId = comment.BlogId,
                        UserEmail = user.Name
                    });
                }
            }

            if (!result.Any())
                return NotFound();

            return CreatedAtAction("GetBlogComments", result);
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
          if (_context.Comment == null)
          {
              return NotFound();
          }
            var comment = await _context.Comment.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(CommentDTO commentDTO)
        {
            if (_context.Comment == null)
            {
                return Problem("Entity set 'DataContext.Comments'  is null.");
            }

            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == commentDTO.UserEmail);

            if (user == null)
                return NotFound();

            Comment comment = new()
            {
                CommentText = commentDTO.CommentText,
                BlogId = commentDTO.BlogId,
                CommentId = commentDTO.CommentId,
                UserId = user.Id
            };

            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (_context.Comment == null)
            {
                return NotFound();
            }
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return (_context.Comment?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static CommentDTO CommentDTO(Comment comment) =>
            new CommentDTO
            {
                CommentText = comment.CommentText
            };
    }
}
