using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace EFCoreEjemplos
{
    class ApplicationDbContext: DbContext
    {
        public DbSet<Estudiante> Estudiantes { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<EstudianteCurso> EstudiantesCursos { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AspCoreExample;Trusted_Connection=True;MultipleActiveResultSets=true")
                .EnableSensitiveDataLogging(true)
                .UseLoggerFactory(new LoggerFactory().AddConsole((category, level) => level == LogLevel.Information && category == DbLoggerCategory.Database.Command.Name, true));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstudianteCurso>().HasKey(x => new { x.CursoId, x.EstudianteId });
            modelBuilder.Entity<EstudianteCurso>().HasQueryFilter(x => x.Activo);

            // table splitting, una tabla en varias clases
            modelBuilder.Entity<Estudiante>().HasOne(x => x.Detalles).WithOne(x => x.Estudiante).HasForeignKey<EstudianteDetalle>(x => x.Id);
            modelBuilder.Entity<EstudianteDetalle>().ToTable("Estudiantes");

            // mapeo flexible, transformaciones previas a la insercion
            modelBuilder.Entity<Estudiante>().Property(x => x.Apellido).HasField("apellido");
        }

        public override int SaveChanges()
        {
            // borrado suave
            foreach (var item in ChangeTracker.Entries().Where(e=> e.State == EntityState.Deleted && e.Metadata.GetProperties().Any(x => x.Name == "EstaBorrado")))
            {
                item.State = EntityState.Unchanged;
                item.CurrentValues["EstaBorrado"] = true;
            }
            return base.SaveChanges();
        }

        [DbFunction(Schema = "dbo")]
        public static int Cantidad_De_Cursos_Activos(int EstudianteId)
        {
            throw new Exception();
        }
    }
}
