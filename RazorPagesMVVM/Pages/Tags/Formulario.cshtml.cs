using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorPagesMVVM.Models;
using System.Collections.Generic;

namespace RazorPagesMVVM.Pages.Tags
{
    public class FormularioModel : PageModel
    {
        public Persona Persona { get; set; }
        public List<SelectListItem> PreguntasSeguridad
        {
            get
            {
                var escogido = Persona?.Pregunta;
                var preguntas = ObtenerPreguntasDeSeguridad();
                var opciones = new List<SelectListItem>();
                foreach (var pregunta in preguntas)
                {
                    opciones.Add(new SelectListItem()
                    {
                        Text = pregunta,
                        Value = pregunta,
                        Selected = pregunta == escogido
                    });
                }
                return opciones;
            }
        }

        private List<string> ObtenerPreguntasDeSeguridad()
        {
            return new List<string>() { "Cual es la nacionalidad de su perro?", "Pepsi o Coca Cola?", "Nombre de una persona que usted no conoce?"};
        }

        public void OnGet()
        {

        }

        [ValidateAntiForgeryToken]
        public void OnPostConToken()
        {
            if (ModelState.IsValid)
            {
                
            }
        }
    }
}