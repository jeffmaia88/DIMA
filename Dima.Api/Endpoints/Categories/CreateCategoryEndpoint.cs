using Dima.Api.Common.Api;
using Dima.Core.Entities;
using Dima.Core.Responses;
using Dima.Core.Requests.Categories;
using Dima.Core.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Dima.Api.Endpoints.Categories
{
    public class CreateCategoryEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", HandleAsync)
               .WithName("Categories: Create")
               .WithSummary("Cria uma nova categoria")
               .WithDescription("Cria uma nova categoria para o usuário autenticado")
               .WithOrder(1)
               .Produces<Response<Category?>>();
        }

        //herda da Interface IEndpoint, aqui implementa o que será mostrado no FrontEnd
        
        //método que funciona como controller
        private static async Task<IResult> HandleAsync([FromServices]ICategoryHandler handler, [FromBody] CreateCategoryRequest request)
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

