using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiPaises.Data;
using WebApiPaises.Models;

namespace WebApiPaises.Controllers
{
    [Route("api/Pais/{PaisId}/Estado")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private readonly AppDbContext context;

        public EstadoController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<Estado> Get(int paisId)
        {
            return context.Estados.Where(x => x.PaisId == paisId).ToList();
        }

        // postman: https://localhost:44356/api/pais/1
        [HttpGet("{id}", Name = "estadoById")]
        public IActionResult GetById(int id)
        {
            var estado = context.Estados.FirstOrDefault(x => x.Id == id);
            if (estado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(estado);
            }
        }

        // postman: https://localhost:44356/api/pais/1/estado, recordar:
        // body: raw, json = { "nombre": "Barinas" }
        [HttpPost]
        public IActionResult Post([FromBody] Estado estado, int PaisId)
        {
            estado.PaisId = PaisId;
            if (ModelState.IsValid)
            {
                context.Estados.Add(estado);
                context.SaveChanges();
                return new CreatedAtRouteResult("estadoById", new { estado.Id }, estado);
            }
            return BadRequest(ModelState);
        }

        // postman: https://localhost:44356/api/pais/1/estado/(id del recurso a modificar), recordar:
        // body: raw, json = { "id": 7, "nombre": "Barinas updated", "paisId": 1 }
    [HttpPut("{id}")]
        public IActionResult Put([FromBody] Estado estado, int id)
        {
            if (estado.Id != id)
            {
                return BadRequest();
            }

            context.Entry(estado).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        // postman: https://localhost:44356/api/pais/1/estado/(id del recurso a eliminar)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var estado = context.Estados.FirstOrDefault(x => x.Id == id);
            if (estado == null)
            {
                return NotFound();
            }
            else
            {
                context.Estados.Remove(estado);
                context.SaveChanges();
                return Ok(estado);
            }
        }
    }
}