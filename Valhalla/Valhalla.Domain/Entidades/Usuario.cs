using System;
namespace Valhalla.Domain.Entidades
{
	public class Usuario
	{
        public Usuario()
        {
            ID = Guid.NewGuid();
        }
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

