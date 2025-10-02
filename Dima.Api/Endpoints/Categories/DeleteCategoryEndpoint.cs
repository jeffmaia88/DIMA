using Dima.Api.Common.Api;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class DeleteCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", HandleAsync)
              .WithName("Categories: Delete")
              .WithSummary("Deleta uma categoria existente")
              .WithDescription("Deleta uma categoria existente para o usuário autenticado")
              .WithOrder(3)
              .Produces<Response<Category?>>();
        }

        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, [FromServices] ICategoryHandler handler, [FromBody] DeleteCategoryRequest request, [FromRoute]long id)
        {
            request.UserId = user.Identity?.Name ?? string.Empty;
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
