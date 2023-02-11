using System;
using Valhalla.Domain.Enums;

namespace Valhalla.Domain.Models
{
	public class Turno
	{
        public Turno(Rodada rodada, int numeroDoTurno)
        {
            Rodada = rodada;
            NumeroDoTurno = numeroDoTurno;
            ID = Guid.NewGuid();
        }

        public Guid ID { get; set; }
        public DateTime? HoraInicio { get; set; }
        public DateTime? HoraFim { get; set; }
        public Jogador Jogador { get; set; }
        public Rodada Rodada { get; set; }
        public TurnoAcaoEnum Acao { get; set; }
        public Carta Carta { get; set; }
        public int NumeroDoTurno { get; set; }

        public event Action TurnoIniciadoEvent;
        public event Action TurnoFinalizadoComJogadaDeCartaEvent;
        public event Action TurnoFinalizadoComTrocaDeCartaEvent;
        public event Action TurnoFinalizadoComPassagemDaVezEvent;

        public void IniciaTurno(Jogador jogador)
        {
            if (HoraInicio == null)
            {
                HoraInicio = DateTime.Now;
                Jogador = jogador;
            }            
        }

        public void JogaCarta(Carta carta)
        {
            Carta = carta;
            Acao = TurnoAcaoEnum.JogouUmaCarta;            
            FinalizaTurno();
            TurnoFinalizadoComJogadaDeCartaEvent?.Invoke();
        }

        public void TrocaCarta()
        {
            Acao = TurnoAcaoEnum.TrocouUmaCarta;
            FinalizaTurno();
            TurnoFinalizadoComTrocaDeCartaEvent?.Invoke();
        }

        public void PassaAVez()
        {
            Acao = TurnoAcaoEnum.PassouAVez;
            FinalizaTurno();
            TurnoFinalizadoComPassagemDaVezEvent?.Invoke();
        }

        private void FinalizaTurno()
        {
            HoraFim = DateTime.Now;
            Rodada.ProximoTurno(this);
        }

        public override string ToString()
        {
            return "Turno " + NumeroDoTurno;
        }
    }
}

