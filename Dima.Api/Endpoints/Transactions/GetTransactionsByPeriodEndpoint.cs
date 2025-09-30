using Dima.Api.Common.Api;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using Dima.Core;
using Dima.Core.Requests.Transactions;

namespace Dima.Api.Endpoints.Transactions
{
    public class GetTransactionsByPeriodEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
               .WithName("Transactions: Get By Period")
               .WithSummary("Recupera todas as transações")
               .WithDescription("Busca uma Lista com todas as transações")
               .WithOrder(5)
               .Produces<PagedResponse<List<Transaction>>>();  
               
        }

        private static async Task<IResult> HandleAsync([FromServices] ITransactionHandler handler, [FromQuery] DateTime? startDate = null, [FromQuery] DateTime? endDate = null,
                                                       [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                                                       [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetTransactionByPeriodRequest
            {
                UserId = "jeff@balta.io",
                PageNumber = pageNumber,
                PageSize = pageSize,
                StartDate = startDate,
                EndDate = endDate,
            };

            var result = await handler.GetByPeriodAsync(request);
            if (result.IsSuccess) 
            { 
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.NotFound(result);
            }


        }
    }
}
