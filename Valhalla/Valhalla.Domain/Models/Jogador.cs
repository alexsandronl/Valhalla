using System;
namespace Valhalla.Domain.Models
{
	public class Jogador
	{
        public Guid ID { get; set; }
        public string NomeCompleto { get; set; }
		public string PrimeiroNome { get; set; }
		public byte[] Avatar { get; set; }
		public string Identificacao { get; set; }
		public string Email { get; set; }

        public override string ToString()
        {
            return NomeCompleto;
        }
    }
}

