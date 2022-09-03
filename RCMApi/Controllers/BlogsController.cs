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
          if (_context.Blogs == null)
          {
              return NotFound();
          }
            return await _context.Blogs.Select(x => BlogDTO(x)).ToListAsync();
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDTO>> GetBlog(int id)
        {
          if (_context.Blogs == null)
          {
              return NotFound();
          }
            var blog = await _context.Blogs.FindAsync(id);

            if (blog == null)
            {
                return NotFound();
            }

            return BlogDTO(blog);
        }

        // PUT: api/Blogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlog(int id, BlogDTO blogDTO)
        {
            if (id != blogDTO.Id)
            {
                return BadRequest();
            }

            var Blog = await _context.Blogs.FindAsync(id);
            if(Blog == null)
            {
                return NotFound();
            }

            Blog.BlogTitle = blogDTO.BlogTitle;
            Blog.Content = blogDTO.Content;
            Blog.Description = blogDTO.Description;
            Blog.ImagePath = blogDTO.ImagePath;
            Blog.UserId = blogDTO.UserId;

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
        public async Task<ActionResult<BlogDTO>> PostBlog(BlogDTO blogDTO)
        {
            var blog = new Blog
            {
                BlogTitle = blogDTO.BlogTitle,
                Content = blogDTO.Content,
                Description = blogDTO.Description,
                ImagePath = blogDTO.ImagePath,
                UserId = blogDTO.UserId
            };

            if (_context.Blogs == null)
            {
                return Problem("Entity set 'DataContext.Blogs'  is null.");
            }
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlog", new { id = blog.Id }, BlogDTO(blog));
        }

        // DELETE: api/Blogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            if (_context.Blogs == null)
            {
                return NotFound();
            }
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BlogExists(int id)
        {
            return (_context.Blogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static BlogDTO BlogDTO(Blog blog) =>
            new BlogDTO
            {
                Id = blog.Id,
                BlogTitle = blog.BlogTitle,
                Content = blog.Content,
                ImagePath = blog.ImagePath,
                Description = blog.Description,
                UserId = blog.UserId
            };
    }
}
