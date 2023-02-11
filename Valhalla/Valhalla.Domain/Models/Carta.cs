using System;
using Valhalla.Domain.Enums;

namespace Valhalla.Domain.Models
{
    public class Carta
    {
        public Carta(Valhalla.Domain.Entidades.Carta carta, byte[] imagemCarta)
        {
            Descricao = carta.Descricao;
            Titulo = carta.Titulo;
            Cargo = carta.Cargo;
            Setor = carta.Setor;
            Ponto = carta.Ponto;
            AtributosEspeciais = new List<AtributoEspecial>();
            carta.AtributosEspeciais.ForEach(p => AtributosEspeciais.Add(new AtributoEspecial() { Titulo = p.Titulo, PontoAdicional = p.PontoAdicional }));
            Imagem = imagemCarta;
        }

        public Guid ID { get; set; }
        public string Cargo { get; set; }
        public string Setor { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public byte[] Imagem { get; set; }
        public int Ponto { get; set; }

        public List<AtributoEspecial> AtributosEspeciais { get; set; }
        public int PontoTotal => Ponto + PontoExtra;
        public int PontoExtra => AtributosEspeciais.Sum(p => p.PontoAdicional);


        public override string ToString()
        {
            return Titulo;
        }
    }
}

