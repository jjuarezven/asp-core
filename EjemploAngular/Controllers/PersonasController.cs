using EjemploAngular.Data;
using EjemploAngular.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjemploAngular.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PersonasController : ControllerBase
	{
		private readonly ApplicationDbContext context;

		public PersonasController(ApplicationDbContext context)
		{
			this.context = context;
		}

		// GET: api/Personas
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Persona>>> GetPersonas()
		{
			//return await Task.Run(() => new List<Persona>
			//{
			//	new Persona { Id = 1, Nombre = "Pedro P", FechaNacimiento = new DateTime(1978,11,22) },
			//	new Persona { Id = 1, Nombre = "Luis A", FechaNacimiento = new DateTime(1976,1,22) }
			//});
			
			return await context.Personas.ToListAsync();
		}

		// GET: api/Personas/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetPersona(int id, bool incluirDirecciones = false)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			Persona persona;
			if (incluirDirecciones)
			{
				persona = await context.Personas.Include(x => x.Direcciones).SingleOrDefaultAsync(x => x.Id == id);
			}
			else
			{
				persona = await context.Personas.SingleOrDefaultAsync(x => x.Id == id);
			}
			

			if (persona == null)
			{
				return NotFound();
			}

			return Ok(persona);
		}

		// PUT: api/Personas/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutPersona(int id, Persona persona)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != persona.Id)
			{
				return BadRequest();
			}

			context.Entry(persona).State = EntityState.Modified;

			try
			{
				await CrearOEditarDirecciones(persona.Direcciones);
				await context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!PersonaExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}
			return NoContent();
		}

		private async Task CrearOEditarDirecciones(List<Address> direcciones)
		{
			var direccionesACrear = direcciones.Where(x => x.Id == 0).ToList();
			var direccionesAEditar = direcciones.Where(x => x.Id != 0).ToList();

			if (direccionesACrear.Any())
			{
				await context.AddRangeAsync(direccionesACrear);
			}

			if (direccionesAEditar.Any())
			{
				context.UpdateRange(direccionesAEditar);
			}
		}

		// POST: api/Personas
		[HttpPost]
		public async Task<ActionResult<Persona>> PostPersona([FromBody] Persona persona)
		{
			context.Personas.Add(persona);
			await context.SaveChangesAsync();

			return CreatedAtAction("GetPersona", new { id = persona.Id }, persona);
		}

		// DELETE: api/Personas/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<Persona>> DeletePersona(int id)
		{
			var persona = await context.Personas.FindAsync(id);
			if (persona == null)
			{
				return NotFound();
			}

			context.Personas.Remove(persona);
			await context.SaveChangesAsync();

			return persona;
		}

		private bool PersonaExists(int id)
		{
			return context.Personas.Any(e => e.Id == id);
		}
	}
}
