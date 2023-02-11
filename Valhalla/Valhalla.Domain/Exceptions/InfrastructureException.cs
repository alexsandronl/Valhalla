namespace Valhalla.Domain.Exceptions
{
    public class InfrastructureException : Exception
    {
        public InfrastructureException(string businessMessage)
               : base(businessMessage)
        {
        }
    }
}
