using HolaMundo.Models;
using System.Collections.Generic;

namespace HolaMundo.Services
{
    public interface IRepositorioPais
	{
		IEnumerable<Pais> ObtenerTodos();
	}
}
