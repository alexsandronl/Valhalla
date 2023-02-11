using System;

namespace Valhalla.Domain.Entidades
{
    public class Carta
    {
        public Carta()
        {
            AtributosEspeciais = new List<AtributoEspecial>();
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public string Cargo { get; set; }
        public string Setor { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string NomeImagem { get; set; }
        public int Ponto { get; set; }

        public List<AtributoEspecial> AtributosEspeciais { get; set; }

        public int PontoTotal => Ponto + AtributosEspeciais.Sum(p => p.PontoAdicional);

    }
}

