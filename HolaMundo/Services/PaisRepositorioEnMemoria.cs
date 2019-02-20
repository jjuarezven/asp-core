using HolaMundo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolaMundo.Services
{
	public class PaisRepositorioEnMemoria : IRepositorioPais
	{
		public IEnumerable<Pais> ObtenerTodos() 
		{
			return new List<Pais>() { new Pais("Venezuela"), new Pais("Colombia"), new Pais("Peru") };
		}
	}
}
