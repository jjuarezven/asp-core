using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EjemploAngular.Data;
using EjemploAngular.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EjemploAngular.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DireccionesController : ControllerBase
	{
		private readonly ApplicationDbContext context;

		public DireccionesController(ApplicationDbContext context)
		{
			this.context = context;
		}

		[HttpPost("delete/list")]
		public IActionResult DeleteList([FromBody] List<int> ids)
		{
			try
			{
				var direcciones = ids.Select(id => new Address { Id = id }).ToList();
				context.RemoveRange(direcciones);
				context.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			return Ok();
		}
	}
}