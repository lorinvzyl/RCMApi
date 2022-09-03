using Microsoft.EntityFrameworkCore;
using RCMAppApi.Models;

namespace RCMAppApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Blog>? Blogs { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<Event>? Events { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<UserEvent>? UserEvents { get; set; }
        public DbSet<UserRole>? UserRoles { get; set; }
        public DbSet<Video>? Videos { get; set; }
        public DbSet<Donation>? Donations { get; set; }
        public DbSet<Booking>? Bookings { get; set; }

    }
}
