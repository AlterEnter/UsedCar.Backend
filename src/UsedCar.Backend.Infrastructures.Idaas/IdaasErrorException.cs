namespace UsedCar.Backend.Infrastructures.Idaas
{
    public class IdaasErrorException : Exception
    {
        public IdaasErrorException(string message, Exception innerException)

            : base(message, innerException)
        {

        }

    }
}
