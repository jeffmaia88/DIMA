using Dima.Api.Common.Api;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;


namespace Dima.Api.Endpoints.Categories


{
    public class UpdateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/{id}", HandleAsync)
                .WithName("Categories: Update")
                .WithSummary("Atualiza uma categoria existente")
                .WithDescription("Atualiza uma categoria existente para o usuário autenticado")
                .WithOrder(2)
                .Produces<Response<Category?>>();
        }


        private static async Task<IResult> HandleAsync([FromServices]ICategoryHandler handler, [FromBody]UpdateCategoryRequest request,[FromRoute] long id)
        {
            request.Id = id;

            var result = await handler.UpdateAsync(request);
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
