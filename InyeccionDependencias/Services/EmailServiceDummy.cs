namespace InyeccionDependencias.Services
{
    public class EmailServiceDummy : IEmailService
    {
        public string EnviarCorreo()
        {
            return "Mensaje enviado";
        }
    }
}
