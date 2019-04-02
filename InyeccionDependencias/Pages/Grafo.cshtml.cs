using InyeccionDependencias.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InyeccionDependencias.Pages
{
    public class GrafoModel : PageModel
    {
        private IServicioA servicioA;

        public string MensajeDelServicio { get; private set; }

        public GrafoModel(IServicioA servicioA)
        {
            this.servicioA = servicioA;
            MensajeDelServicio = servicioA.ObtenerMensaje();
        }

        public void OnGet()
        {

        }
    }
}