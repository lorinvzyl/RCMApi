using Microsoft.EntityFrameworkCore;
using RCMAppApi.Models;
using RCMApi.Models;

namespace RCMAppApi.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Blog>? Blog { get; set; }
        public DbSet<Comment>? Comment { get; set; }
        public DbSet<Role>? Role { get; set; }
        public DbSet<Event>? Event { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet<UserEvent>? UserEvent { get; set; }
        public DbSet<UserRole>? UserRole { get; set; }
        public DbSet<Video>? Video { get; set; }
        public DbSet<Donation>? Donation { get; set; }
        public DbSet<Booking>? Booking { get; set; }
        public DbSet<Location>? Location { get; set; }

    }
}
