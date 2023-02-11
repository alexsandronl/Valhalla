using System;
using Valhalla.Domain.Enums;

namespace Valhalla.Domain.Models
{
    public class AtributoEspecial
    {
        public string Titulo { get; set; }
        public int PontoAdicional { get; set; }

        public override string ToString()
        {
            return Titulo;
        }
    }
}

