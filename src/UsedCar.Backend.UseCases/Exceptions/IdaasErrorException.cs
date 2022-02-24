namespace UsedCar.Backend.UseCases.Exceptions
{
    public class IdaasErrorException : Exception
    {
        public IdaasErrorException(string message, Exception innerException)

            : base(message, innerException)
        {

        }

    }
}
