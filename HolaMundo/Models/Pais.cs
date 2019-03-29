namespace HolaMundo.Models
{
    public class Pais
	{
		public int Id { get; set; }
		public string Nombre { get; set; }
		public Pais()
		{

		}
		public Pais(string nombre)
		{
			Nombre = nombre;
		}
	}
}
