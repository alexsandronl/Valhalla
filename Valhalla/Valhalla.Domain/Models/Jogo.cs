using System;
using Valhalla.Domain.Enums;

namespace Valhalla.Domain.Models
{
    public class Jogo
    {
        public Jogo(Jogador jogador01, Jogador jogador02, int numeroDeCartasIniciais, int numeroDeRodadas, int numeroDeTurnos, TimeSpan tempoporturno, Tabuleiro tabuleiro)
        {
            ID = Guid.NewGuid();
            Jogador01 = jogador01;
            Jogador02 = jogador02;
            NumeroDeCartasIniciais = numeroDeCartasIniciais;
            NumeroDeRodadas = numeroDeRodadas;
            NumeroDeTurnos = numeroDeTurnos;
            TempoPorTurno = tempoporturno;
            Tabuleiro = tabuleiro;

            for (int r = 1; r < numeroDeRodadas + 1; r++)
            {
                var rodada = new Rodada(this, r);
                Rodadas.Add(rodada);
            }
        }

        public Guid ID { get; set; }

        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public TimeSpan TempoPorTurno { get; set; }
        public Jogador Jogador01 { get; set; }
        public Jogador Jogador02 { get; set; }
        public Jogador JogadorQueAbandonou { get; set; }
        public int PontosParciaisTotalJogador01 => Rodadas.Where(p => p.Status == StatusRodadaEnum.Finalizada).Sum(a => a.PontosDoJogador01);
        public int PontosParciaisTotalJogador02 => Rodadas.Where(p => p.Status == StatusRodadaEnum.Finalizada).Sum(a => a.PontosDoJogador02);
        public int? PontosTotalJogador01 { get; set; }
        public int? PontosTotalJogador02 { get; set; }
        public int NumeroDePartidasGanhasDoJogador01 => Rodadas.Count(p => p.Status == StatusRodadaEnum.Finalizada && p.JogadorGanhadorDaRodada == Jogador01);
        public int NumeroDePartidasGanhasDoJogador02 => Rodadas.Count(p => p.Status == StatusRodadaEnum.Finalizada && p.JogadorGanhadorDaRodada == Jogador02);
        public Tabuleiro Tabuleiro { get; set; }
        public PartidaStatusEnum StatusDaPartida { get; set; }
        public event Action IniciaPartidaEvent;
        public event Action TerminaPartidaEvent;
        public event Action AbandonouPartidaEvent;

        public TimeSpan TempoDeJogo
        {
            get
            {
                if (DataInicio == null && DataFim == null)
                    return new TimeSpan();

                if (DataInicio != null && DataFim != null)
                    return DataFim.Value - DataInicio.Value;

                return DateTime.Now - DataInicio.Value;
            }
        }

        public List<Rodada> Rodadas { get; set; } = new List<Rodada>();
        public Rodada RodadaAtual => Rodadas.SingleOrDefault(p => p.Status == StatusRodadaEnum.Iniciada);

        public int NumeroDeCartasIniciais { get; set; }
        public int NumeroDeRodadas { get; set; }
        public int NumeroDeTurnos { get; set; }

        public Jogador JogadorGanhador
        {
            get
            {
                if (this.StatusDaPartida != PartidaStatusEnum.Finalizada && this.StatusDaPartida != PartidaStatusEnum.Abandonado)
                    return null;

                if (this.StatusDaPartida == PartidaStatusEnum.Abandonado)
                {
                    if (JogadorQueAbandonou == Jogador01)
                        return Jogador02;
                    else
                        return Jogador01;
                }

                if (NumeroDePartidasGanhasDoJogador01 > NumeroDePartidasGanhasDoJogador02)
                    return Jogador01;

                if (NumeroDePartidasGanhasDoJogador02 > NumeroDePartidasGanhasDoJogador01)
                    return Jogador02;

                return null; //Empate
            }
        }

        public int? PontosDoJogadorGanhador
        {
            get
            {
                if (this.StatusDaPartida != PartidaStatusEnum.Finalizada && this.StatusDaPartida != PartidaStatusEnum.Abandonado)
                    return null;

                if (PontosTotalJogador01 > PontosTotalJogador02)
                    return PontosTotalJogador01;

                if (PontosTotalJogador02 > PontosTotalJogador01)
                    return PontosTotalJogador02;

                return PontosTotalJogador01; //Empate
            }
        }

        public void IniciaPartida()
        {
            if (StatusDaPartida == PartidaStatusEnum.AguardandoInicio)
            {
                DataInicio = DateTime.Now;
                StatusDaPartida = PartidaStatusEnum.Iniciada;
                IniciaPartidaEvent?.Invoke();
                ProximaRodada();
            }
        }

        public void TerminaPartida()
        {
            if (StatusDaPartida == PartidaStatusEnum.Iniciada)
            {
                DataFim = DateTime.Now;
                StatusDaPartida = PartidaStatusEnum.Finalizada;
                CalculaPontuacaoFinal();

                //Tabuleiro.Jogador01Deck.Clear();
                //Tabuleiro.Jogador02Deck.Clear();

                //Tabuleiro.Jogador01Mesa.Clear();
                //Tabuleiro.Jogador02Mesa.Clear();
                TerminaPartidaEvent?.Invoke();
            }
        }

        public void AbandonouPartida(Jogador jogadorQueAbandonou)
        {
            if (StatusDaPartida == PartidaStatusEnum.Iniciada)
            {
                DataFim = DateTime.Now;
                StatusDaPartida = PartidaStatusEnum.Abandonado;
                JogadorQueAbandonou = jogadorQueAbandonou;
                CalculaPontuacaoFinal();
                AbandonouPartidaEvent?.Invoke();
            }
        }

        public void ProximaRodada(Rodada ultimaRodada = null)
        {
            if (Rodadas.Count(p => p.Status == StatusRodadaEnum.NaoIniciada) == 0)
            {
                TerminaPartida();
            }
            else if (Rodadas.Count(p => p.Status == StatusRodadaEnum.NaoIniciada) == Rodadas.Count)
            {
                var proximaRodada = Rodadas.OrderBy(p => p.NumeroDaRodada).First();
                proximaRodada.IniciaRodada();
            }
            else
            {
                Tabuleiro.SinalizaProximaRodada();
                Task.Delay(7000);
                var proximaRodada = Rodadas.Where(p => p.Status == StatusRodadaEnum.NaoIniciada).OrderBy(p => p.NumeroDaRodada).First();
                proximaRodada.IniciaRodada();
            }
        }

        private void CalculaPontuacaoFinal()
        {
            if (JogadorQueAbandonou != null)
            { //Abandono de Jogo
                if (Jogador01 == JogadorQueAbandonou)
                {
                    PontosTotalJogador01 = 0;
                    PontosTotalJogador02 = PontosParciaisTotalJogador02;
                }

                if (Jogador02 == JogadorQueAbandonou)
                {
                    PontosTotalJogador01 = PontosParciaisTotalJogador01;
                    PontosTotalJogador02 = 0;
                }
            }
            else
            { //Finalizacao de Jogo
                PontosTotalJogador01 = PontosParciaisTotalJogador01;
                PontosTotalJogador02 = PontosParciaisTotalJogador02;

                if (JogadorGanhador != null)
                { //Um dos jogadores ganhou
                    if (JogadorGanhador == Jogador01 && Tabuleiro.Jogador01Deck.Count > 0)
                    {
                        PontosTotalJogador01 += 50 + Tabuleiro.Jogador01Deck.Sum(p => p.Ponto);
                    }
                    else
                    {
                        PontosTotalJogador02 += 50 + Tabuleiro.Jogador02Deck.Sum(p => p.Ponto);
                    }
                }
                else
                { //Deu empate
                    PontosTotalJogador01 += Tabuleiro.Jogador01Deck.Sum(p => p.Ponto);
                    PontosTotalJogador02 += Tabuleiro.Jogador02Deck.Sum(p => p.Ponto);
                }
            }
        }

        public override string ToString()
        {
            return "Jogo " + ID;
        }
    }
}

