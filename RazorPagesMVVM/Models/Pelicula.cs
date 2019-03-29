using System;

namespace RazorPagesMVVM.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Estreno { get; set; }
        public bool EnCartelera { get; set; }
    }
}
