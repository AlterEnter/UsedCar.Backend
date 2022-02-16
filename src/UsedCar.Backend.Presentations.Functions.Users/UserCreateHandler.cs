using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
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
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")] HttpRequestData req)
        {
            HttpResponseData response = req.CreateResponse(HttpStatusCode.Created);

            UserCreateRequest userCreateRequest = new UserCreateRequest()
            {
                Address = "hoge",
                City = "hoge",
                DateOfBirth = DateTime.Now,
                DisplayName = "hoge",
                IDassId = Guid.NewGuid().ToString(),
                MailAddress = "test@example.com",
                FirstName = "Red",
                LastName = "Blue",
                PhoneNumber = "09099999999",
                State = "hoge",
                Street1 = "hoge",
                Street2 = "hoge",
                UserId = Guid.NewGuid(),
                Zip = "hoge"
            };
            await _userCreateUseCase.ExecuteAsync(userCreateRequest);
            return response;
        }
    }
}
