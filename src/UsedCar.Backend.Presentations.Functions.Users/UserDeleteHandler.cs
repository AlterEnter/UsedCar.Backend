using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using UsedCar.Backend.Presentations.Functions.Core.Authorizations;
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
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            try
            {
                iDassId = new TokenResolver(claimsPrincipal).GetId();
            }
            catch (InvalidOperationException)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }

            try
            {
                await _userDeleteUseCase.ExecuteAsync(iDassId);

            }
            catch (DbException)
            {
                response.StatusCode = HttpStatusCode.ServiceUnavailable;
            }
            catch (IdaasErrorException)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            catch (UserForbiddenException)
            {
                response.StatusCode =HttpStatusCode.Forbidden;
            }

            return response;

        }
    }
}
