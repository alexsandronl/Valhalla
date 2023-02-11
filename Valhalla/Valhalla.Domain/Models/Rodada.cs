using System;
using Valhalla.Domain.Enums;

namespace Valhalla.Domain.Models
{
    public class Rodada
    {
        public Rodada(Jogo jogo, int numeroDaRodada)
        {
            Jogo = jogo;
            NumeroDaRodada = numeroDaRodada;
            ID = Guid.NewGuid();
            ListaDeTurnos = new List<Turno>();

            for (int t = 1; t < jogo.NumeroDeTurnos + 1; t++)
            {
                ListaDeTurnos.Add(new Turno(this,t));
            }
        }

        public Guid ID { get; set; }
        public StatusRodadaEnum Status { get; set; }
        public int NumeroDaRodada { get; set; }
        public int PontosDoJogador01 { get; set; }
        public int PontosDoJogador02 { get; set; }
        public Jogador JogadorGanhadorDaRodada { get; set; }
        public Jogo Jogo { get; set; }
        public List<Turno> ListaDeTurnos { get; set; }
        public Jogador JogadorQueComeca { get; private set; }
        public Turno TurnoAtual => ListaDeTurnos.SingleOrDefault(p => p.HoraInicio != null && p.HoraFim == null);

        public event Action IniciaRodadaEvent;
        public event Action ProximoTurnoEvent;
        public event Action FinalizaRodadaEvent;

        public async void IniciaRodada()
        {
            if (Status == StatusRodadaEnum.NaoIniciada)
            {
                Status = StatusRodadaEnum.Iniciada;
                Jogo.Tabuleiro.Jogador01Mesa.Clear();
                Jogo.Tabuleiro.Jogador02Mesa.Clear();
                IniciaRodadaEvent?.Invoke();
                CalculaRodada();
                ProximoTurno();
            }
        }

        public void ProximoTurno(Turno ultimoTurno = null)
        {
            CalculaRodada();

            if (ListaDeTurnos.Count(p => p.HoraInicio == null) == 0)
            {
                FinalizaRodada();
            }
            else if (ListaDeTurnos.Count(p => p.HoraInicio == null) == ListaDeTurnos.Count)
            {
                //Sorteia quem vai começar
                var queminicia = new Random().Next(1, 2);
                JogadorQueComeca = queminicia == 1 ? Jogo.Jogador01 : Jogo.Jogador02;

                //Começa o primeiro turno da rodada
                var primeiroturno = ListaDeTurnos.First();
                primeiroturno.IniciaTurno(JogadorQueComeca);
            }
            else
            {
                var proximoturno = ListaDeTurnos.First(p => p.HoraInicio == null);
                proximoturno.IniciaTurno(ultimoTurno.Jogador == Jogo.Jogador01 ? Jogo.Jogador02 : Jogo.Jogador01);
            }
            ProximoTurnoEvent?.Invoke();
        }

        public void FinalizaRodada()
        {
            Status = StatusRodadaEnum.Finalizada;
            CalculaRodada();

            if (PontosDoJogador01 > PontosDoJogador02)
                JogadorGanhadorDaRodada = Jogo.Jogador01;
            else if (PontosDoJogador02 > PontosDoJogador01)
                JogadorGanhadorDaRodada = Jogo.Jogador02;
            else
                JogadorGanhadorDaRodada = null;

            FinalizaRodadaEvent?.Invoke();

            Jogo.ProximaRodada(this);
        }

        public void CalculaRodada()
        {
            int pontoJogador01 = 0;
            int pontoJogador02 = 0;

            var listaCartasDaRodadaJogador01 = ListaDeTurnos.Where(p => p.HoraFim != null && p.Jogador == Jogo.Jogador01 && p.Acao == TurnoAcaoEnum.JogouUmaCarta).Select(a => a.Carta);
            var listaCartasDaRodadaJogador02 = ListaDeTurnos.Where(p => p.HoraFim != null && p.Jogador == Jogo.Jogador02 && p.Acao == TurnoAcaoEnum.JogouUmaCarta).Select(a => a.Carta);

            pontoJogador01 = listaCartasDaRodadaJogador01.Sum(p => p.PontoTotal);
            pontoJogador02 = listaCartasDaRodadaJogador02.Sum(p => p.PontoTotal);

            PontosDoJogador01 = pontoJogador01;
            PontosDoJogador02 = pontoJogador02;
        }

        public override string ToString()
        {
            return "Rodada " + NumeroDaRodada;
        }
    }
}

