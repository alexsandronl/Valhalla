using System;
namespace Valhalla.Domain.Models
{
    public class JogadorRanking
    {
        public JogadorRanking(string jogadornome, int pontuacaototal, DateTime ultimojogo, int numerodejogos) => (JogadorNome, PontuacaoTotal, UltimoJogo, NumeroDeJogos) = (jogadornome, pontuacaototal, ultimojogo, numerodejogos);

        public string JogadorNome { get; set; }
        public int PontuacaoTotal { get; set; }
        public DateTime UltimoJogo { get; set; }
        public int NumeroDeJogos { get; set; }
    }
}

