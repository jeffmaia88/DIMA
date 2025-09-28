using Dima.Api.Common.Api;
using Dima.Core;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Categories
{
    public class GetAllCategoriesEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", HandleAsync)
                .WithName("Categories: GetAll")
               .WithSummary("Obtém todas as categorias")
               .WithDescription("Obtém todas as categorias existentes para o usuário autenticado")
               .WithOrder(5)
               .Produces<PagedResponse<List<Category>>>();
        }

        private static async Task<IResult> HandleAsync(ICategoryHandler handler, 
                                                      [FromQuery] int pageNumber = Configuration.DefaultPageNumber,
                                                      [FromQuery] int pageSize = Configuration.DefaultPageSize)
        {
            var request = new GetAllCategoriesRequest
            {
                UserId = "jeff@balta.io",
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await handler.GetAllAsync(request);
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
