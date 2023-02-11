using System;
namespace Valhalla.Domain.Models
{
	public class UsuarioAutenticado
	{
        public Guid ID { get; set; }
        public string Identificador { get; set; }
		public string NomeCompleto { get; set; }
		public string PrimeiroNome { get; set; }
		public string Email { get; set; }

        public override string ToString()
        {
            return NomeCompleto;
        }
    }
}

