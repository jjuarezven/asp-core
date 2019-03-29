using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesMVVM.Models;

namespace RazorPagesMVVM.Pages
{
    // IndexModel es un viewModel y le podemos agregar las propiedades que consideremos necesarias
    // por eso agregamos Persona como una propiedad
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Persona Persona { get; set; }
        [BindProperty]
        public Ciudad Ciudad { get; set; }
        [TempData]
        public string Nombre { get; set; }

        public IActionResult OnPost(string txtSimple)
        {
            Nombre = txtSimple;
            return RedirectToPage("Privacy");
        }

        public void OnPostPersona()
        {
        }

        public void OnPostPersonaCiudad()
        {
        }
    }
}
