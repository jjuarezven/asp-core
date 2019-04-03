using InyeccionDependencias.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Serilog;

namespace InyeccionDependencias.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly Serilog.ILogger seriLog;

        //private readonly IEmailService emailService;
        public string Mensaje { get; set; }

        //public IndexModel(IEmailService emailService)
        //{
        //    this.emailService = emailService;
        //}

        public IndexModel(ILogger<IndexModel> logger, Serilog.ILogger seriLog)
        {
            this.logger = logger;
            this.seriLog = seriLog;
        }

        // tambien se pueden inyectar dependencias en los métodos del controlador
        public void OnGet([FromServices] IEmailService emailService)
        {
            Mensaje = emailService.EnviarCorreo();
            logger.LogDebug("Este es un mensaje debug");
            logger.LogWarning("Este es un mensaje de warning");
            logger.LogError("Este es un mensaje de error");
            seriLog.Error("Este es un mensaje de error");
        }
    }
}
