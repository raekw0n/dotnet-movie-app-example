// A database context class is needed to coordinate EF Core functionality (Create, Read, Update, Delete) for the Movie model.
// The database context is derived from Microsoft.EntityFrameworkCore.DbContext and specifies the entities to include in the data model.

using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movie { get; set; }
    }
}