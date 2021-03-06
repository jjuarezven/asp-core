﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApiPaises.Data;
using WebApiPaises.Models;

namespace WebApiPaises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaisController : ControllerBase
    {
        private readonly AppDbContext context;

        public PaisController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var claims = User.Claims.ToList();
            var esAdmin = claims.Any(x => x.Type == "Admin" && x.Value == "Y");
            if (!esAdmin)
            {
                var pais = claims.FirstOrDefault(x => x.Type == "Pais");
                if (pais == null)
                {
                    return Unauthorized();
                }
                return Ok(context.Paises.Where(x => x.Nombre == pais.Value));
            }
            return Ok(context.Paises.ToList());
        }

        [HttpGet("{id}", Name = "paisCreado")]
        public IActionResult GetById(int id)
        {
            var pais = context.Paises.Include(x => x.Estados).FirstOrDefault(x => x.Id == id);
            if (pais == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(pais);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Pais pais)
        {
            if (ModelState.IsValid)
            {
                context.Paises.Add(pais);
                context.SaveChanges();
                return new CreatedAtRouteResult("paisCreado", new { pais.Id }, pais);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] Pais pais, int id)
        {
            if (pais.Id != id)
            {
                return BadRequest();
            }

            context.Entry(pais).State = EntityState.Modified;
            context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pais = context.Paises.FirstOrDefault(x => x.Id == id);
            if (pais == null)
            {
                return NotFound();
            }
            else
            {
                context.Paises.Remove(pais);
                context.SaveChanges();
                return Ok(pais);
            }
        }
    }
}