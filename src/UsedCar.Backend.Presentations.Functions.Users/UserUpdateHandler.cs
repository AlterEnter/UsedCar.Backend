using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using UsedCar.Backend.Presentations.Functions.Core.Authorizations;
using UsedCar.Backend.Presentations.Functions.Users.Models;
using UsedCar.Backend.UseCases.Exceptions;
using UsedCar.Backend.UseCases.Users;
using UsedCar.Backend.Presentations.Functions.Users.Validations;

namespace UsedCar.Backend.Presentations.Functions.Users
{
    public class UserUpdateHandler
    {
        private readonly IUserUpdateUseCase _userUpdateUseCase;

        private readonly IMapper _mapper;

        public UserUpdateHandler(IUserUpdateUseCase userUpdateUseCase, IMapper mapper)
        {
            _userUpdateUseCase = userUpdateUseCase;
            _mapper = mapper;
        }

        [Function($"{nameof(Users)}.{nameof(UserUpdateHandler)}")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "users")] HttpRequestData req, FunctionContext context)
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
                var validateResult = await req.GetRequestBodyAsync<UserUpdateRequest>();
                if (validateResult.IsValid is false)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return response;
                }

                var userUpdateRequest = validateResult.Value;

                var userUpdateRequestUseCase = _mapper.Map<UseCases.Users.Models.UserUpdateRequest>(userUpdateRequest);
                userUpdateRequestUseCase.IdaasId = iDassId;
                await _userUpdateUseCase.ExecuteAsync(userUpdateRequestUseCase);
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
