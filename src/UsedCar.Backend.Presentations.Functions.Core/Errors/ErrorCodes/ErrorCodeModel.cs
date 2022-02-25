namespace UsedCar.Backend.Presentations.Functions.Core.Errors.ErrorCodes
{
    public class ErrorCodeModel
    {
        public ErrorCodeModel()
        {
        }

        public ErrorCodeModel(string errorCode)
        {
            ErrorCode = errorCode;
        }
        public string? ErrorCode { get;}
    }
}
