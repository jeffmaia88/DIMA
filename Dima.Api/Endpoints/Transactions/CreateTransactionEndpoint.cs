using Dima.Api.Common.Api;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions
{
    public class CreateTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", HandleAsync)
                .WithName("Transacations: Create")
                .WithSummary("Cria uma nova transação")
                .WithDescription("Cria uma nova transação")
                .WithOrder(1)
                .Produces<Response<Transaction?>>();

        }



        private static async Task<IResult> HandleAsync([FromServices] ITransactionHandler handler, [FromBody] CreateTransactionRequest request)
        {
            request.UserId = "jeff@balta.io";
            var result = await handler.CreateAsync(request);
            if (result.IsSuccess)
            {
                return TypedResults.Created($"/{result.Data?.Id}", result);
            }
            else
            {
                return TypedResults.BadRequest(result.Data);
            }


        }
    }
}
