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
    public class UserReadHandler
    {
        private readonly IUserReadUseCase _userReadUseCase;

        public UserReadHandler(IUserReadUseCase userReadUseCase)
        {
            _userReadUseCase = userReadUseCase;
        }

        [Function($"{nameof(Users)}.{nameof(UserReadHandler)}")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequestData req, FunctionContext context)
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
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
                var userReadResponse = await _userReadUseCase.ExecuteAsync(iDassId);
                await response.WriteAsJsonAsync(userReadResponse);
                return response;
            }
            catch (IdaasNotFoundException)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
        }
    }
}
