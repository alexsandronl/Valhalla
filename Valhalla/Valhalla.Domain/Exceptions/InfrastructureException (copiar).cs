﻿namespace Valhalla.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string businessMessage)
               : base(businessMessage)
        {
        }
    }
}
