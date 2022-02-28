namespace UsedCar.Backend.Presentations.Functions.Core.Errors.ErrorCodes
{
    public class UsersErrorCodeFactory
    {
        private const string ErrorCodePrefix = "Users";
        private const string NoIdaasInfoErrorCodePostfix = "NoIdaasInfo";
        private const string NoUserInfoErrorCodePostfix = "NoUserInfo";

        public class Unauthorized
        {
            public static ErrorCodeModel Create() => new($"{ErrorCodePrefix}.{nameof(Unauthorized)}");
        }

        public class BadRequest
        {
            public static ErrorCodeModel Create() => new($"{ErrorCodePrefix}.{nameof(BadRequest)}");
        }

        public class NotFound
        {
            public static ErrorCodeModel Create() => new($"{ErrorCodePrefix}.{nameof(NotFound)}");
        }

        public class Forbidden
        { 
            public static ErrorCodeModel CreateNoIdaasInfo() => new($"{ErrorCodePrefix}.{nameof(Forbidden)}.{NoIdaasInfoErrorCodePostfix}");
            public static ErrorCodeModel CreateNoUserInfo() =>
                new($"{ErrorCodePrefix}.{nameof(Forbidden)}.{NoUserInfoErrorCodePostfix}");
        }

        public class DdError
        {
            public static ErrorCodeModel Create() => new($"{ErrorCodePrefix}.{nameof(DdError)}");
        }

        public class IdaasError
        {
            public static ErrorCodeModel Create() => new($"{ErrorCodePrefix}.{nameof(IdaasError)}");
        }
    }

        



    
}
