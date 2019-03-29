using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiPaises.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [ForeignKey("Pais")]
        public int PaisId { get; set; }
        [JsonIgnore]
        public Pais Pais { get; set; }
    }
}
