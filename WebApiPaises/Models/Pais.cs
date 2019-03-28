using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApiPaises.Models
{
    public class Pais
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string Nombre { get; set; }
        public List<Estado> Estados { get; set; }

        public Pais()
        {
            Estados = new List<Estado>();
        }
    }
}
