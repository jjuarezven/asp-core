using HolaMundo.Data;
using HolaMundo.Models;
using System.Collections.Generic;
using System.Linq;

namespace HolaMundo.Services
{
    public class PaisRepositorioEF : IRepositorioPais
	{
		private ApplicationDbContext DbContext { get; }
		public PaisRepositorioEF(ApplicationDbContext dbContext)
		{
			DbContext = dbContext;
		}

		public IEnumerable<Pais> ObtenerTodos()
		{
			return DbContext.Paises.ToList();
		}
	}
}
