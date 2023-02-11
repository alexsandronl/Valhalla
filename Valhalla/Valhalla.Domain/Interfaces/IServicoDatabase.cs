using System;
using System.Text.Json;
using Valhalla.Domain.Entidades;

namespace Valhalla.Domain.Interfaces
{
	public interface IServicoDatabase
	{
        public List<Carta> BuscaTodasCartas();
        public void AtualizarCartas(List<Carta> listaDeCartas);
        public void SalvaImagemCarta(string nomeArquivo, byte[] imagem);
        public byte[] BuscaImagemCarta(string nomeArquivo);
        public List<Usuario> BuscaTodosUsuarios();
        public void AdicionaNovoUsuario(Usuario usuario);
        public List<Jogo> BuscaTodosJogos();
        public void AdicionaNovoJogo(Jogo jogo);
    }
}

