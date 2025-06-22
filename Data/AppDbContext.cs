using Microsoft.EntityFrameworkCore;
using TrackItApp.Models;

namespace TrackItApp.Data
{
    // This class handles communication between the application and the database
    public class AppDbContext : DbContext
    {
        // Constructor that receives configuration options and passes them to the base class
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // This DbSet represents the Users table in the database
        public DbSet<User> Users { get; set; }
    }
}

