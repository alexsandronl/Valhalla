using System;
using System.Text.Json;
using System.Linq;
using Valhalla.Domain.Entidades;
using Valhalla.Domain.Interfaces;

namespace Valhalla.Infraestrutura.Servicos
{
	public class ServicoDatabase : IServicoDatabase
    {
		//TODO: Futuramente implementar banco de dados real

		private string CaminhoDatabase { get; set; }
        private string CaminhoFotoCartas { get; set; }
        private string CaminhoCartas { get; set; }
		private string CaminhoUsuarios { get; set; }
		private string CaminhoJogos { get; set; }

		public ServicoDatabase()
		{
			CaminhoDatabase = Environment.GetEnvironmentVariable("CaminhoDatabase");
            Directory.CreateDirectory(CaminhoDatabase);

            CaminhoFotoCartas = Path.Combine(CaminhoDatabase, "FotosCartas");
            Directory.CreateDirectory(CaminhoFotoCartas);

            CaminhoCartas = Path.Combine(CaminhoDatabase, "Cartas.json");
            if (!File.Exists(CaminhoCartas))
            File.WriteAllText(CaminhoCartas, JsonSerializer.Serialize(new List<Carta>()));

            CaminhoUsuarios = Path.Combine(CaminhoDatabase, "Usuarios.json");
            if (!File.Exists(CaminhoUsuarios))
                File.WriteAllText(CaminhoUsuarios, JsonSerializer.Serialize(new List<Usuario>()));

            CaminhoJogos = Path.Combine(CaminhoDatabase, "Jogos.json");
            if (!File.Exists(CaminhoJogos))
                File.WriteAllText(CaminhoJogos, JsonSerializer.Serialize(new List<Jogo>()));

        }

        #region Cartas

        public List<Carta> BuscaTodasCartas()
        {
            var lista = JsonSerializer.Deserialize<List<Carta>>(File.ReadAllText(CaminhoCartas));
            return lista;
        }

        public void AtualizarCartas(List<Carta> listaDeCartas)
        {
            SalvaTodasCartas(listaDeCartas);
        }

        private void SalvaTodasCartas(List<Carta> listaDeCartas)
        {
            File.WriteAllText(CaminhoCartas, JsonSerializer.Serialize(listaDeCartas.OrderBy(p => p.Titulo).ToList()));
        }

        public void SalvaImagemCarta(string nomeArquivo, byte[] imagem)
        {
            File.WriteAllBytes(Path.Combine(CaminhoFotoCartas, nomeArquivo), imagem);
        }

        public byte[] BuscaImagemCarta(string nomeArquivo)
        {
            return File.ReadAllBytes(Path.Combine(CaminhoFotoCartas, nomeArquivo));
        }

        #endregion

        #region Usuarios

        public List<Usuario> BuscaTodosUsuarios()
        {
            var lista = JsonSerializer.Deserialize<List<Usuario>>(File.ReadAllText(CaminhoUsuarios));
            return lista;
        }

        public void AdicionaNovoUsuario(Usuario usuario)
        {
            var lista = BuscaTodosUsuarios();
            lista.Add(usuario);
            File.WriteAllText(CaminhoUsuarios, JsonSerializer.Serialize(lista.OrderBy(p => p.NomeCompleto).ToList()));
        }

        #endregion

        #region Jogos

        public List<Jogo> BuscaTodosJogos()
        {
            var lista = JsonSerializer.Deserialize<List<Jogo>>(File.ReadAllText(CaminhoJogos));
            return lista;
        }

        public void AdicionaNovoJogo(Jogo jogo)
        {
            var lista = BuscaTodosJogos();
            lista.Add(jogo);
            File.WriteAllText(CaminhoJogos, JsonSerializer.Serialize(lista.OrderBy(p => p.DataHoraDoJogo).ToList()));
        }

        #endregion

    }
}

