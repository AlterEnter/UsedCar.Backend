using System.Security.Claims;
using Microsoft.Azure.Functions.Worker;

namespace UsedCar.Backend.Presentations.Functions.Core.Authorizations;

public class ClaimsPrincipalResolver
{
    private readonly FunctionContext _context;

    public ClaimsPrincipalResolver(FunctionContext context) => _context = context;

    public ClaimsPrincipal? GetClaimsPrincipal() => _context.Features.Get<ClaimsPrincipal>();
}