using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreEjemplos
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //Estudiante jose;
            //using (var context = new ApplicationDbContext())
            //{
            //    jose = context.Estudiantes.Where(x => x.Nombre == "Jose Juarez").FirstOrDefault();
            //}

            Console.ReadKey();
        }

        private static void MapeoFlexible()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = new Estudiante
                {
                    Nombre = "Ra fael",
                    Edad = 43,
                    EstaBorrado = false,
                    InstitucionId = 1,
                    Apellido = "juarez"
                };
                var d = new EstudianteDetalle();
                estudiante.Detalles = d;
                context.Add(estudiante);

                context.SaveChanges();
            }
        }

        private static void InsertandoSplittedTable()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = new Estudiante
                {
                    Nombre = "Ra mon",
                    Edad = 43,
                    EstaBorrado = false,
                    InstitucionId = 1
                };

                estudiante.Detalles = new EstudianteDetalle
                {
                    Becado = true,
                    Carrera = "Informatica",
                    CategoriaDePago = 1
                };
                context.Add(estudiante);
                context.SaveChanges();

                // asi se consulta, si no se usa Include no trae el Detalle
                var estudiantes = context.Estudiantes.Include(x => x.Detalles).ToList();
            }
        }

        private static void UsandoFuncionesEscalares()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiantes = context.Estudiantes.Where(x => ApplicationDbContext.Cantidad_De_Cursos_Activos(x.Id) > 0).ToList();
            }
        }

        private static void BorradoSuave()
        {
            using (var context = new ApplicationDbContext())
            {
                context.Remove(context.Estudiantes.FirstOrDefault());
                context.SaveChanges();
            }
        }

        private static void AvoidSqlInjectionWithStringInterpolation()
        {
            using (var context = new ApplicationDbContext())
            {
                var nombre = "'Jose' or 1=1";
                var estudiante = context.Estudiantes.FromSql($"select * from estudiantes where nombre = {nombre}").ToList();
            }
        }

        private static void AplicandoHasQueryFilter()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiantesCursos = context.EstudiantesCursos.ToList();
            }
        }

        private static void ConsultandMuchosAMuchos()
        {
            using (var context = new ApplicationDbContext())
            {
                var curso = context.Cursos.Where(x => x.Id == 1).Include(x => x.EstudiantesCursos)
                    .ThenInclude(y => y.Estudiante).FirstOrDefault();
            }
        }

        private static void InsertandoMuchosAMuchos()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = context.Estudiantes.FirstOrDefault();
                var curso = context.Cursos.FirstOrDefault();
                var estudianteCurso = new EstudianteCurso
                {
                    EstudianteId = estudiante.Id,
                    CursoId = curso.Id,
                    Activo = true
                };
                context.Add(estudianteCurso);
                context.SaveChanges();
            }
        }

        private static void ConsultandoUnoAMuchos()
        {
            using (var context = new ApplicationDbContext())
            {
                // traera todos los estudiantes de la institucion
                // var institucion = context.Instituciones.Where(x => x.Id == 1).Include(x => x.Estudiantes).ToList();
                var institucionesEstudiantes = context.Instituciones.Where(x => x.Id == 1)
                    .Select(x => new { Institucion = x, Estudiantes = x.Estudiantes.Where(e => e.Edad > 18).ToList() }).ToList();
            }
        }

        private static void AgregarUnoAUnoDesconectado()
        {
            var estudianteJose = new Estudiante
            {
                Id = 1
            };
            var direccion = new Direccion
            {
                Calle = "Valencia",
                EstudianteId = estudianteJose.Id
            };
            using (var context = new ApplicationDbContext())
            {
                context.Direcciones.Add(direccion);
                context.SaveChanges();
            }
        }

        private static void AgregarUnoAUnoConectado()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = new Estudiante
                {
                    Nombre = "Rafael",
                    Edad = 25
                };

                var direccion = new Direccion
                {
                    Calle = "Caracas"
                };
                estudiante.Direccion = direccion;
                context.Add(estudiante);
                context.SaveChanges();
            }
        }

        // simula entorno web: cada peticion usa un db context diferente
        private static void ActualizarModeloDesconectado(Estudiante estudiante)
        {
            estudiante.Nombre += " Rodriguez";
            using (var context = new ApplicationDbContext())
            {
                context.Entry(estudiante).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        private static void ActualizarModeloConectado()
        {
            using (var context = new ApplicationDbContext())
            {
                var estudiante = context.Estudiantes.Where(x => x.Nombre == "Claudia Lopez").ToList();
                estudiante[0].Nombre += " Chacon";

                context.SaveChanges();
            }
        }

        private static void InsertarEstudiante()
        {
            using (var context = new ApplicationDbContext())
            {
                context.Estudiantes.Add(new Estudiante { Nombre = "Claudia Lopez" });
                context.SaveChanges();
            }
        }
    }

    class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public int InstitucionId { get; set; }
        public bool EstaBorrado { get; set; }
        private string apellido;

        public string Apellido
        {
            get { return apellido; }
            set { apellido = value.ToUpper(); }
        }

        public Direccion Direccion { get; set; }
        public List<EstudianteCurso> EstudiantesCursos { get; set; }
        public EstudianteDetalle Detalles { get; set; }
    }

    class EstudianteDetalle
    {
        public int Id { get; set; }
        public bool Becado { get; set; }
        public string Carrera { get; set; }
        public int CategoriaDePago { get; set; }
        public Estudiante Estudiante { get; set; }
    }

    class Direccion
    {
        public int Id { get; set; }
        public string Calle { get; set; }
        public int EstudianteId { get; set; }
    }

    class Institucion
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Estudiante> Estudiantes { get; set; }
    }

    // muchos a muchos
    class Curso
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<EstudianteCurso> EstudiantesCursos { get; set; }
    }

    class EstudianteCurso
    {
        public int EstudianteId { get; set; }
        public int CursoId { get; set; }
        public bool Activo { get; set; }
        public Estudiante Estudiante { get; set; }
        public Curso Curso { get; set; }
    }


}
