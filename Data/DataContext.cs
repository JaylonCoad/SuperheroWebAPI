using Microsoft.EntityFrameworkCore;
using SuperheroWebAPI.Entities;

namespace SuperheroWebAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Superhero> superheroes { get; set; } // superheroes is a list of type Superhero from DB
    }
}
