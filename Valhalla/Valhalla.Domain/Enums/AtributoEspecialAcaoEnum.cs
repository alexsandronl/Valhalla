using System;
namespace Valhalla.Domain.Enums
{
	public enum AtributoEspecialAcaoEnum
	{
		AdicionaPontoParaTodos = 0,
		AdicionaPontoParaUmTipoDeCargo = 1,
		AdicionaPontoParaUmSetor = 2,

		RemovePontoParaTodos = 10,
		RemovePontoParaUmTipoDeCargo = 11,
		RemovePontoParaUmSetor = 12,

		RemovePontoParaTodosDoAdversario = 20,
		RemovePontoParaUmTipoDeCargoDoAdversario = 21,
		RemovePontoParaUmSetorDoAdversario = 22
	}
}

