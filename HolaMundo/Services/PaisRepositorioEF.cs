using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HolaMundo.Data;
using HolaMundo.Models;
using Microsoft.EntityFrameworkCore;

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
