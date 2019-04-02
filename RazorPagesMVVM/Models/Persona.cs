using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMVVM.Models
{
    public class Persona
    {
        public int Id { get; set; }
        [BindRequired]
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(20)]
        public string Nombre { get; set; }
        [BindRequired]
        [Range(0, 120)]
        public int? Edad { get; set; }
        public Pais Pais { get; set; }
        public string Pregunta { get; set; }
        public string Descripcion { get; set; }
    }

    public enum Pais
    {
        [Display(Name = "Escoger")]
        Desconocido = 0,
        Venezuela = 1,
        Colombia = 2
    }
}
