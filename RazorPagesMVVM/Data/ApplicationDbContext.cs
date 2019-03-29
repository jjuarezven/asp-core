using Microsoft.EntityFrameworkCore;
using RazorPagesMVVM.Models;

namespace RazorPagesMVVM.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<Pelicula> Peliculas { get; set; }
    }
}
