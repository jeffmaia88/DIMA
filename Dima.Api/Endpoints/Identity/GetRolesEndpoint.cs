using Dima.Api.Common.Api;
using Dima.Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Identity
{
    public class GetRolesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/roles", Handle)
               .WithName("Identity/Logout")
               .RequireAuthorization();


        }

        private static Task<IResult> Handle(ClaimsPrincipal user)
        {
            if (user.Identity is null || !user.Identity.IsAuthenticated)
                return Task.FromResult(Results.Unauthorized());

            var roles = user.FindAll(ClaimTypes.Role)
                                .Select(c => new
                                {
                                    c.Issuer,
                                    c.OriginalIssuer,
                                    c.Type,
                                    c.Value,
                                    c.ValueType
                                });

            return Task.FromResult<IResult>(TypedResults.Json(roles));
        }
    }
}

