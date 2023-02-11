using System;
using Valhalla.Domain.Enums;
using Valhalla.Domain.Models;

namespace Valhalla.Domain.Entidades
{
    public class AtributoEspecial
    {
        public AtributoEspecial()
        {
            PontoAdicional = 1;
        }

        public string Titulo { get; set; }
        public int PontoAdicional { get; set; }

        public override string ToString()
        {
            return Titulo;
        }
    }
}

