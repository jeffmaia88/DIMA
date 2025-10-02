using Dima.Api.Common.Api;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetTransactionByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync)
               .WithName("Transacations: Get By Id")
               .WithSummary("Recupera uma transação")
               .WithDescription("Recupera uma transação")
               .WithOrder(4)
               .Produces<Response<Transaction?>>();
        }


        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, [FromServices] ITransactionHandler handler, [FromRoute]long id)
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id

            };

            var result = await handler.GetByIdAsync(request);
            if(result.IsSuccess)
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(result);
            }
        }
    }
}
