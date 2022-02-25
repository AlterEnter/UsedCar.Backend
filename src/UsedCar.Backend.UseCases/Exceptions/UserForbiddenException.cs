namespace UsedCar.Backend.UseCases.Exceptions
{
    public class UserForbiddenException : Exception
    {
        public enum ForbiddenVariation
        {
            NoIdaasInfo,
            NoUserInfo
        }

        public ForbiddenVariation Variation { get; }

        public UserForbiddenException(ForbiddenVariation forbiddenVariation) => Variation = forbiddenVariation;

    }
}
