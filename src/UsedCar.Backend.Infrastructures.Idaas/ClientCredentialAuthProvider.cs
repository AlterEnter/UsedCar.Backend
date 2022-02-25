using System.Net.Http.Headers;
using Azure.Core.Pipeline;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Protocols;

namespace UsedCar.Backend.Infrastructures.Idaas
{
    public class ClientCredentialAuthProvider : IAuthenticationProvider
    {
        private readonly IConfidentialClientApplication _msalClient;
        private  const int maxtries = 3;


        public ClientCredentialAuthProvider(string applicationId, string tenantId, string? secret)
        {
            _msalClient = ConfidentialClientApplicationBuilder.Create(applicationId)
                .WithTenantId(tenantId).WithClientSecret(secret).Build();
        }

        public async Task AuthenticateRequestAsync(HttpRequestMessage request)
        {
            int retryTimes = 0;

            do
            {
                try
                {
                    AuthenticationResult? result = await _msalClient
                        .AcquireTokenForClient(new[] {"https://graph.microsoft.com/.default"}).ExecuteAsync();
                    if (!string.IsNullOrEmpty(result.AccessToken))
                    {
                        request.Headers.Authorization = new AuthenticationHeaderValue("bearer", result.AccessToken);
                        break;
                    }
                }
                catch (MsalServiceException e)
                {
                    if (e.ErrorCode == "temporarily_unavailable")
                    {
                        TimeSpan delay = GetRetryAfter(e);
                        await Task.Delay(delay);
                    }
                    else
                    {
                        throw new AuthenticationException(
                            new Error
                            {
                                Code = "generalException",
                                Message = "Unexpected exception returned from MSAL."
                            },
                            e
                        );
                    }
                }
                catch (AuthenticationException e)
                {
                    throw new ArgumentException(e.Message);
                }
                catch (Exception exception)
                {
                    throw new AuthenticationException(
                        new Error
                        {
                            Code = "generalException",
                            Message = "Unexpected exception occurred while authenticating the request."
                        },
                        exception);
                }
                retryTimes++;
            } while (retryTimes < maxtries);
        }
        private static TimeSpan GetRetryAfter(MsalServiceException serviceException)
        {
            RetryConditionHeaderValue? retryAfter = serviceException.Headers?.RetryAfter;
            TimeSpan? delay = null;

            if (retryAfter != null && retryAfter.Delta.HasValue)
            {
                delay = retryAfter.Delta;
            }
            else if (retryAfter != null && retryAfter.Date.HasValue)
            {
                delay = retryAfter.Date.Value.Offset;
            }

            return delay == null
                ? throw new MsalServiceException(
                    serviceException.ErrorCode,
                    "Missing Retry-After header."
                )
                : delay.Value;
        }
    }
}
