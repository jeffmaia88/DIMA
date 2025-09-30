using Dima.Api.Common.Api;
using Dima.Api.Handlers;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests.Transactions;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Transactions
{
    public class DeleteTransactionEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", HandleAsync)
               .WithName("Transactions: Delete")
               .WithSummary("Exclui uma Transação")
               .WithDescription("Exclui uma Transação")
               .WithOrder(2)
               .Produces <Response<Transaction>>();
             
        }

        private static async Task<IResult> HandleAsync ([FromServices]ITransactionHandler handler, [FromBody] DeleteTransactionRequest request, [FromRoute]long id)
        {
            request.UserId = "jeff@balta.io";
            request.Id = id;
            var result = await handler.DeleteAsync(request);
            if (result.IsSuccess)
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
