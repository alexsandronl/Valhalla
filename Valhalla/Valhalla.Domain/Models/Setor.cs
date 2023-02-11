using System;
namespace Valhalla.Domain.Models
{
	public class Setor
	{
		public Setor(string descricao)
        {
            ID = Guid.NewGuid();
            Descricao = descricao;
        }

        public Guid ID { get; set; }
        public string Descricao { get; private set; }

        public override string ToString()
        {
            return Descricao;
        }
    }
}

