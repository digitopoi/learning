using Microsoft.EntityFrameworkCore;

namespace RazorPagesMovie.Models
{
    public class MovieContext : DbContext
    {
        public DbSet<Movie> Movie { get; set; }
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) {}
    }
}