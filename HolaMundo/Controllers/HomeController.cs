using HolaMundo.Models;
using HolaMundo.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HolaMundo.Controllers
{
    public class HomeController : Controller
	{
        // la inyeccion de dependencia se hace en Startup.cs, metodo ConfigureServices => services.AddScoped<IRepositorioPais, PaisRepositorioEF>();
        private readonly IRepositorioPais repositorio;

		public HomeController(IRepositorioPais repositorio)
		{
			this.repositorio = repositorio;
		}

		public IActionResult Index()
		{
			var paises = this.repositorio.ObtenerTodos();
			return View(paises);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
