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
    public class BlogsController : ControllerBase
    {
        private readonly DataContext _context;

        public BlogsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> GetBlogs()
        {
            if (_context.Blog == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog.ToListAsync();

            IEnumerable<BlogDTO> result = new List<BlogDTO>();

            foreach(var item in blog)
            {
                var author = await _context.User.FindAsync(item.AuthorId);
                result.Append(new BlogDTO
                {
                    Id = item.Id,
                    Content = item.Content,
                    Author = author.Name,
                    BlogTitle = item.BlogTitle,
                    Description = item.Description,
                    ImagePath = item.ImagePath
                });
            }

            return CreatedAtAction("GetBlogs", result);
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDTO>> GetBlog(int id)
        {
          if (_context.Blog == null)
          {
              return NotFound();
          }
            var blog = await _context.Blog.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return BlogDTO(blog);
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, Blog blog)
        {
            if (id != blog.Id)
            {
                return BadRequest();
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogExists(id))
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

        // POST: api/Blogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Blog>> PostBlog(Blog blog)
        {
          if (_context.Blog == null)
          {
              return Problem("Entity set 'DataContext.Blogs'  is null.");
          }
            _context.Blog.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.Id }, blog);
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (_context.Blog == null)
            {
                return NotFound();
            }
            var blog = await _context.Blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blog.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogExists(int id)
        {
            return (_context.Blog?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static BlogDTO BlogDTO(Blog blog) =>
            new BlogDTO
            {
                Content = blog.Content,
                ImagePath = blog.ImagePath,
                Description = blog.Description,
                BlogTitle = blog.BlogTitle,
            };
    }
}
