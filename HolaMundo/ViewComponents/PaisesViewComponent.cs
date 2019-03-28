using HolaMundo.Services;
using Microsoft.AspNetCore.Mvc;

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
