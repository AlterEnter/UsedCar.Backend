using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using UsedCar.Backend.Presentations.Functions.Core.Authorizations;
using UsedCar.Backend.UseCases.Exceptions;
using UsedCar.Backend.UseCases.Users;
using UsedCar.Backend.UseCases.Users.Models;

namespace UsedCar.Backend.Presentations.Functions.Users
{
    public class UserCreateHandler
    {
        private readonly IUserCreateUseCase _userCreateUseCase;

        public UserCreateHandler(IUserCreateUseCase userCreateUseCase)
        {
            _userCreateUseCase = userCreateUseCase;
        }



        [FunctionName($"{nameof(Users)}.{nameof(UserCreateHandler)}")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")] HttpRequestData req, FunctionContext context)
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.Created);
            ClaimsPrincipal? claimsPrincipal = new ClaimsPrincipalResolver(context).GetClaimsPrincipal();

            if (claimsPrincipal == null)
            {
                response.StatusCode = HttpStatusCode.Unauthorized;
                return response;
            }
            try
            {
                string iDassId = new TokenResolver(claimsPrincipal).GetId();
            }
            catch (InvalidOperationException)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
            UserCreateRequest userCreateRequest = new()
            {
                City = "例：広島市",
                DateOfBirth = DateTime.Parse(new TokenResolver(claimsPrincipal).GetDateOfBirth()),
                DisplayName = new TokenResolver(claimsPrincipal).GetDisplayName(),
                IDassId = new TokenResolver(claimsPrincipal).GetId(),
                MailAddress = new TokenResolver(claimsPrincipal).GetMailAddress(),
                FirstName = "例：太郎",
                LastName = "例：中古",
                PhoneNumber = new TokenResolver(claimsPrincipal).GetPhoneNumber(),
                State = "例；広島県",
                Street1 = "例：国泰寺町",
                Street2 = "例：1丁目6-34 市役所",
                Zip = "例：730-8586"
            };
            try
            {
                await _userCreateUseCase.ExecuteAsync(userCreateRequest);
                return response;
            }
            catch (DuplicatedUserException)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                return response;
            }
        }
    }
}
