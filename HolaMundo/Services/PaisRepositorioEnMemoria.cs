using HolaMundo.Models;
using System.Collections.Generic;

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
