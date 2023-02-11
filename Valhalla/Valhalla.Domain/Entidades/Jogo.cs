using System;
namespace Valhalla.Domain.Entidades
{
    public class Jogo
    {
        public Jogo()
        {
            ID = Guid.NewGuid();
        }
        public Guid ID { get; set; }
        public DateTime DataHoraDoJogo { get; set; }
        public Usuario Jogador01 { get; set; }
        public Usuario Jogador02 { get; set; }
        public JogoStatusFinalEnum JogoStatusFinal { get; set; }
        public int PontosJogador01 { get; set; }
        public int PontosJogador02 { get; set; }
        public TimeSpan TempoDePartida { get; set; }

        public enum JogoStatusFinalEnum
        {
            Ganhou = 0,
            Perdeu = 1,
            Empatou = 2
        }
    }
}

