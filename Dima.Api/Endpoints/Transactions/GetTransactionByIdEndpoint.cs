using Dima.Api.Common.Api;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

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


        private static async Task<IResult> HandleAsync([FromServices] ITransactionHandler handler, [FromRoute]long id)
        {
            var request = new GetTransactionByIdRequest
            {
                UserId = "jeff@balta.io",
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
