namespace Valhalla.Domain.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(string businessMessage)
               : base(businessMessage)
        {
        }
    }
}
