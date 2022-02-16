using System.Security.Claims;

namespace UsedCar.Backend.Presentations.Functions.Core.Authorizations
{
    public class TokenResolver
    {
        private readonly ClaimsPrincipal _user;

        public TokenResolver(ClaimsPrincipal user) => _user = user;

        public string GetId() => _user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new InvalidOperationException("ClaimTypes.NameIdentifierが取得できません");

        public string GetMailAddress() => _user.Claims.FirstOrDefault(c => c.Type == "emails")?.Value ?? throw new InvalidOperationException("ClaimsPrincipalからemailsが取得できません");

        public string GetDisplayName() => _user.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? throw new InvalidOperationException("ClaimsPrincipalからnameが取得できません");

        public string GetDateOfBirth() => _user.FindFirst(ClaimTypes.DateOfBirth)?.Value ?? throw new InvalidOperationException("ClaimsTypes.DateOfBirthが取得できません");

        public string GetPhoneNumber() => _user.FindFirst(ClaimTypes.MobilePhone)?.Value ??
                                          throw new InvalidOperationException("ClaimsTypes.MobilePhoneが取得できません");
    }
}
