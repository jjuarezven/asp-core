using InyeccionDependencias.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace InyeccionDependencias.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly IEmailService emailService;
        public string Mensaje { get; set; }

        //public IndexModel(IEmailService emailService)
        //{
        //    this.emailService = emailService;
        //}

        // tambien se pueden inyectar dependencias en los métodos del controlador
        public void OnGet([FromServices] IEmailService emailService)
        {
            Mensaje = emailService.EnviarCorreo();
        }
    }
}
