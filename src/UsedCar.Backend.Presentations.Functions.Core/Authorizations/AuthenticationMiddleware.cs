using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using UsedCar.Backend.LoggerExtensions;
using UsedCar.Backend.Presentations.Functions.Core.FunctionContexts;


namespace UsedCar.Backend.Presentations.Functions.Core.Authorizations;

public class AuthenticationMiddleware : IFunctionsWorkerMiddleware
{
    private readonly IConfiguration _configuration;

    public AuthenticationMiddleware(IConfiguration configuration) => _configuration = configuration;

    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            var httpRequestData = context.GetHttpRequestData();
            var url = httpRequestData?.Url.ToString();

            if (string.IsNullOrEmpty(url))
            {
                return;
            }

            if (httpRequestData?.Headers == null)
            {
                return;
            }

            var accessToken = httpRequestData.Headers.GetValues("Authorization")?.FirstOrDefault();

            if (string.IsNullOrEmpty(accessToken) ||
                accessToken.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) is false)
            {
                return;
            }

            accessToken = accessToken.Substring("Bearer ".Length).Trim();
            var principal = await GetClaimsPrincipal(accessToken);
            context.Features.Set(principal);

        }
        catch (Exception ex)
        {
            ILogger<AuthenticationMiddleware>? log = context.GetLogger<AuthenticationMiddleware>();
            log.AccessTokenInvalid(ex);

        }
        finally
        {
            await next(context);
        }
    }

    private async Task<ClaimsPrincipal> GetClaimsPrincipal(string header)
    {
        var audience = _configuration["IDaaS:Audience"];
        var metadataEndpoint = _configuration["IDaaS:MetadataEndpoint"];
        var accessToken = header;

        var handler = new JwtSecurityTokenHandler();

        var result = handler.CanReadToken(accessToken);

        if (!result)
        {
            throw new ArgumentException("token");
        }

        IConfigurationManager<OpenIdConnectConfiguration> configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(metadataEndpoint, new OpenIdConnectConfigurationRetriever());
        var openIdConfig = await configurationManager.GetConfigurationAsync(CancellationToken.None);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = openIdConfig.Issuer,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKeys = openIdConfig.SigningKeys,
            ValidateAudience = true,
            ValidAudience = audience,
            NameClaimType = ClaimTypes.NameIdentifier,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.Zero,
        };

        var principal = handler.ValidateToken(accessToken, validationParameters, out _);

        return principal;
    }
}
