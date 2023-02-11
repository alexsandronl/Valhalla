using System;
using System.Net.WebSockets;
using Autofac;
using Valhalla.Domain.Enums;
using Valhalla.Domain.Interfaces;

namespace Valhalla.Domain.Models
{
    public class Tabuleiro
    {
        private IServicoDatabase servicoDatabase { get; set; }
        private List<Carta> ListaDeTodasAsCartas { get; set; }

        /// <summary>
        /// Inicia um tabuleiro de jogo
        /// </summary>
        /// <param name="jogador01">Jogador 01</param>
        /// <param name="jogador02">Jogador 02</param>
        /// <param name="numeroCartasIniciais">Número de cartas que irá começar no deck de cada jogador</param>
        /// <param name="numeroRodadas">Número de rodadas do jogo</param>
        /// <param name="numeroTurnos">Número de turnos por rodara (Deverá ser número par)</param>
        /// <param name="tempoPorTurno">Tempo que irá durar cada turno</param>
        public Tabuleiro(IServicoDatabase servicodatabase, Jogador jogador01, Jogador jogador02, int numeroCartasIniciais, int numeroRodadas, int numeroTurnos, TimeSpan tempoPorTurno, Guid id)
        {
            if (numeroCartasIniciais < 1)
                throw new Exception("O número de cartas por jogador deverá ser no mínimo uma carta");

            if (numeroRodadas < 1)
                throw new Exception("O número de rodadas do jogo deverá ser no mínimo um");

            if (numeroTurnos < 2)
                throw new Exception("O número de turnos por rodada deverá ser no mínimo dois");

            if (numeroTurnos % 2 != 0)
                throw new Exception("O número de turnos por rodada deverá ser um número par");

            if (tempoPorTurno.TotalSeconds < 5)
                throw new Exception("O tempo por turno deverá ser no mínimo 5 segundos");

            servicoDatabase = servicodatabase;

            var todasascartasdobanco = servicoDatabase.BuscaTodasCartas();
            ListaDeTodasAsCartas = new List<Carta>();
            foreach (var cartabanco in todasascartasdobanco)
            {
                var imagem = servicoDatabase.BuscaImagemCarta(cartabanco.NomeImagem);
                ListaDeTodasAsCartas.Add(new Carta(cartabanco, imagem));
            }

            ID = id;
            Jogo = new Jogo(jogador01, jogador02, numeroCartasIniciais, numeroRodadas, numeroTurnos, tempoPorTurno, this);

            AtualizacaoDePresencaDeJogadores = new Timer(e => VerificaJogadoresAtivos(), new AutoResetEvent(false), 1000, 10000);
        }

        public Guid ID { get; set; }

        public Jogo Jogo { get; set; }

        public DateTime? UltimaPingadaJogador01 { get; set; }
        public DateTime? UltimaPingadaJogador02 { get; set; }
        private Timer AtualizacaoDePresencaDeJogadores;

        public bool Jogador01ProntoParaComecar { get; set; }
        public bool Jogador02ProntoParaComecar { get; set; }

        public event Action AdicionaUmaCartaNoDeckDoJogador01Event;
        public event Action AdicionaUmaCartaNoDeckDoJogador02Event;
        public event Action JogaUmaCartaJogador01Event;
        public event Action JogaUmaCartaJogador02Event;
        public event Action TrocaUmaCartaJogador01Event;
        public event Action TrocaUmaCartaJogador02Event;
        public event Action PassaAVezJogador01Event;
        public event Action PassaAVezJogador02Event;
        public event Action AIniciarProximaRodadaEvent;

        public Rodada RodadaAtual => Jogo.RodadaAtual;
        public Turno TurnoAtual => RodadaAtual?.TurnoAtual;

        public TimeSpan TempoTotalDeJogo
        {
            get
            {
                if (Jogo == null || Jogo.StatusDaPartida == PartidaStatusEnum.AguardandoInicio)
                    return new TimeSpan();

                if (Jogo.StatusDaPartida == PartidaStatusEnum.Iniciada)
                    return DateTime.Now - Jogo.DataInicio.Value;

                if (Jogo.StatusDaPartida == PartidaStatusEnum.Finalizada || Jogo.StatusDaPartida == PartidaStatusEnum.Abandonado)
                    return Jogo.DataFim.Value - Jogo.DataInicio.Value;

                return new TimeSpan();
            }
        }

        public void IniciaJogo()
        {
            if (Jogador01ProntoParaComecar == false || Jogador02ProntoParaComecar == false)
                return;

            SorteiaCartasParaComecar();
            Jogo.IniciaPartida();
        }

        public void SinalizaProximaRodada()
        {
            AIniciarProximaRodadaEvent?.Invoke();
        }

        private void SorteiaCartasParaComecar()
        {
            //Fazer calculo de peso para equilibrar o jogo
            for (int i = 0; i < Jogo.NumeroDeCartasIniciais; i++)
            {
                AdicionaUmaCartaNoDeckDoJogador01();
                AdicionaUmaCartaNoDeckDoJogador02();
            }
        }

        private Carta SorteiaCartasAleatoria()
        {
            var indexcarta = new Random().Next(0, ListaDeTodasAsCartas.Count);
            return ListaDeTodasAsCartas.ToArray()[indexcarta];
        }

        public void AdicionaUmaCartaNoDeckDoJogador01()
        {
            Jogador01Deck.Add(SorteiaCartasAleatoria());
            AdicionaUmaCartaNoDeckDoJogador01Event?.Invoke();
        }

        public void AdicionaUmaCartaNoDeckDoJogador02()
        {
            Jogador02Deck.Add(SorteiaCartasAleatoria());
            AdicionaUmaCartaNoDeckDoJogador02Event?.Invoke();
        }

        #region Ações do Jogador

        public void JogaUmaCartaJogador01(Carta carta)
        {
            if (TurnoAtual.Jogador == Jogo.Jogador01)
            {
                Jogador01Deck.Remove(carta);
                Jogador01Mesa.Add(carta);
                TurnoAtual.JogaCarta(carta);
                if (RodadaAtual != null)
                    Jogo?.RodadaAtual.CalculaRodada();
                JogaUmaCartaJogador01Event?.Invoke();
            }
        }

        public void RepoeUmaCartaNoDeckJogador01(Carta carta)
        {
            Jogador01Deck.Add(carta);
        }

        public void RepoeUmaCartaNoDeckJogador02(Carta carta)
        {
            Jogador02Deck.Add(carta);
        }

        public void JogaUmaCartaJogador02(Carta carta)
        {
            if (TurnoAtual.Jogador == Jogo.Jogador02)
            {
                Jogador02Deck.Remove(carta);
                Jogador02Mesa.Add(carta);
                TurnoAtual.JogaCarta(carta);
                if (RodadaAtual != null)
                    Jogo?.RodadaAtual.CalculaRodada();
                JogaUmaCartaJogador02Event?.Invoke();
            }
        }

        public void TrocaUmaCartaJogador01(Carta carta)
        {
            if (TurnoAtual.Jogador == Jogo.Jogador01)
            {
                Jogador01Deck.Remove(carta);
                Jogador01Deck.Add(SorteiaCartasAleatoria());
                TurnoAtual.TrocaCarta();
                if (RodadaAtual != null)
                    Jogo?.RodadaAtual.CalculaRodada();
                TrocaUmaCartaJogador01Event?.Invoke();
            }
        }

        public void TrocaUmaCartaJogador02(Carta carta)
        {
            if (TurnoAtual.Jogador == Jogo.Jogador02)
            {
                Jogador02Deck.Remove(carta);
                Jogador02Deck.Add(SorteiaCartasAleatoria());
                TurnoAtual.TrocaCarta();
                if (RodadaAtual != null)
                    Jogo?.RodadaAtual.CalculaRodada();
                TrocaUmaCartaJogador02Event?.Invoke();
            }
        }

        public void PassaAVezJogador01()
        {
            if (TurnoAtual.Jogador == Jogo.Jogador01)
            {
                TurnoAtual.PassaAVez();
                if (RodadaAtual != null)
                    Jogo?.RodadaAtual.CalculaRodada();
                RepoeUmaCartaNoDeckJogador01(SorteiaCartasAleatoria());
                PassaAVezJogador01Event?.Invoke();
            }
        }

        public void PassaAVezJogador02()
        {
            if (TurnoAtual.Jogador == Jogo.Jogador02)
            {
                TurnoAtual.PassaAVez();
                if (RodadaAtual != null)
                    Jogo?.RodadaAtual.CalculaRodada();
                RepoeUmaCartaNoDeckJogador02(SorteiaCartasAleatoria());
                PassaAVezJogador02Event?.Invoke();
            }
        }

        private void VerificaJogadoresAtivos()
        {
            //if (Jogo.StatusDaPartida != PartidaStatusEnum.Finalizada && Jogo.StatusDaPartida != PartidaStatusEnum.Abandonado && DateTime.Now - UltimaPingadaJogador01 > new TimeSpan(0,0,30))
            //{
            //    if (Jogo.StatusDaPartida == PartidaStatusEnum.Iniciada)
            //    {
            //        Jogo.AbandonouPartida(Jogo.Jogador01);
            //    }
            //    Jogo.Jogador01 = null;
            //}

            //if (Jogo.StatusDaPartida != PartidaStatusEnum.Finalizada && Jogo.StatusDaPartida != PartidaStatusEnum.Abandonado && DateTime.Now - UltimaPingadaJogador02 > new TimeSpan(0, 0, 30))
            //{
            //    if (Jogo.StatusDaPartida == PartidaStatusEnum.Iniciada)
            //    {
            //        Jogo.AbandonouPartida(Jogo.Jogador02);
            //    }
            //    Jogo.Jogador02 = null;
            //}
        }


        #endregion


        #region Jogador 01

        public int PontosJogador01 { get; set; }
        public List<Carta> Jogador01Deck { get; set; } = new List<Carta>();
        public List<Carta> Jogador01Mesa { get; set; } = new List<Carta>();

        #endregion

        #region Jogador 02

        public int PontosJogador02 { get; set; }
        public List<Carta> Jogador02Deck { get; set; } = new List<Carta>();
        public List<Carta> Jogador02Mesa { get; set; } = new List<Carta>();

        #endregion

    }
}

