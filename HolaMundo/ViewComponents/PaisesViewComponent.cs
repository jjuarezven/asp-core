using HolaMundo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HolaMundo.ViewComponents
{
	public class PaisesViewComponent : ViewComponent
	{
		private IRepositorioPais repositorioPais;
		public PaisesViewComponent(IRepositorioPais repositorioPais)
		{
			this.repositorioPais = repositorioPais;
		}

		public IViewComponentResult Invoke()
		{
			var paises = repositorioPais.ObtenerTodos();
			return View(paises);
		}
	}
}
