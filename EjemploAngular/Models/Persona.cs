using System;
using System.Collections.Generic;

namespace EjemploAngular.Models
{
	public class Persona
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public DateTime FechaNacimiento { get; set; }
		public List<Address> Direcciones { get; set; }
	}
}
