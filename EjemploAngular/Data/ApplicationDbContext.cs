using EjemploAngular.Models;
using Microsoft.EntityFrameworkCore;

namespace EjemploAngular.Data
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<Persona> Personas { get; set; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
	}
}
