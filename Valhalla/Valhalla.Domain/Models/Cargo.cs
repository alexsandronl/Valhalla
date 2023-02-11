using System;
namespace Valhalla.Domain.Models
{
    public class Cargo
    {
        public Cargo(string descricao, Setor setor) => (Descricao, Setor) = (descricao, setor);

        public Guid ID { get; set; } = Guid.NewGuid();
        public string Descricao { get; set; }
        public Setor Setor { get; set; }

        public override string ToString()
        {
            return Descricao;
        }
    }
}

