using Dima.Api.Common.Api;
using Dima.Core.Entities;
using Dima.Core.Handlers;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dima.Api.Endpoints.Categories
{
    public class GetCategoryByIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", HandleAsync)
               .WithName("Categories: GetById")
               .WithSummary("Obtém uma categoria pelo ID")
               .WithDescription("Obtém uma categoria existente para o usuário autenticado pelo ID")
               .WithOrder(4)
               .Produces<Response<Category?>>();
        }  
        
        private static async Task<IResult> HandleAsync(ClaimsPrincipal user, [FromServices] ICategoryHandler handler, [FromRoute] long id)
        {
            var request = new GetCategoryByIdRequest
            {
                UserId = user.Identity?.Name ?? string.Empty,
                Id = id
            };
            
            var result = await handler.GetByIdAsync(request);
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
