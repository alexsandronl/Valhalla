using System;
namespace Valhalla.Infraestrutura.Servicos
{
	public class LinkedInAutenticacao
	{
		public string ClientID { get; set; }
		public string ApiKey { get; set; }
		public LinkedInAutenticacao(string clientid, string apikey) => (ClientID, ApiKey) = (clientid, apikey);

		
	}
}

