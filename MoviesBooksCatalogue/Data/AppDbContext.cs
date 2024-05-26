using Microsoft.EntityFrameworkCore;
using MoviesBooksCatalogue.Models;

namespace MoviesBooksCatalogue.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}