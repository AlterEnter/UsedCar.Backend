using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using UsedCar.Backend.Presentations.Functions.Core.Authorizations;
using UsedCar.Backend.Presentations.Functions.Core.Errors.ErrorCodes;
using UsedCar.Backend.UseCases.Exceptions;
using UsedCar.Backend.UseCases.Users;

namespace UsedCar.Backend.Presentations.Functions.Users
{
    public class UserDeleteHandler
    {
        private readonly IUserDeleteUseCase _userDeleteUseCase;

        public UserDeleteHandler(IUserDeleteUseCase userDeleteUseCase)
        {
            _userDeleteUseCase = userDeleteUseCase;
        }

        [Function($"{nameof(Users)}.{nameof(UserDeleteHandler)}")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "users")] HttpRequestData req, FunctionContext context)
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.Created);
            ClaimsPrincipal? claimsPrincipal = new ClaimsPrincipalResolver(context).GetClaimsPrincipal();

            string iDassId;

            if (claimsPrincipal == null)
            {
                await response.WriteAsJsonAsync(UsersErrorCodeFactory.Unauthorized.Create());
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            try
            {
                iDassId = new TokenResolver(claimsPrincipal).GetId();
            }
            catch (InvalidOperationException)
            {
                await response.WriteAsJsonAsync(UsersErrorCodeFactory.BadRequest.Create());
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                await _userDeleteUseCase.ExecuteAsync(iDassId);
            }
            catch (DbException)
            {
                await response.WriteAsJsonAsync(UsersErrorCodeFactory.DdError.Create());
                response.StatusCode = HttpStatusCode.ServiceUnavailable;
            }
            catch (IdaasErrorException)
            {
                await response.WriteAsJsonAsync(UsersErrorCodeFactory.IdaasError.Create());
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            catch (UserForbiddenException e)
            {
                switch (e.Variation)
                {
                    case UserForbiddenException.ForbiddenVariation.NoIdaasInfo:
                        await response.WriteAsJsonAsync(UsersErrorCodeFactory.Forbidden.CreateNoIdaasInfo());
                        response.StatusCode = HttpStatusCode.Forbidden;
                        return response;
                    case UserForbiddenException.ForbiddenVariation.NoUserInfo:
                        await response.WriteAsJsonAsync(UsersErrorCodeFactory.Forbidden.CreateNoUserInfo());
                        response.StatusCode = HttpStatusCode.Forbidden;
                        return response;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(e.Variation), e.Variation, "ïsê≥Ç»ílÇ≈Ç∑ÅB");
                }

            }
            return response;
        }
    }
}
