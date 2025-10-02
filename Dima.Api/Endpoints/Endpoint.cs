using Dima.Api.Endpoints.Categories;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints.Transactions;

namespace Dima.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
//            var endpoints = app.MapGroup("");

            var categories = app.MapGroup("/v1/categories")
                               .WithTags("Categories")
                               .RequireAuthorization(); // todas rotas de categories precisam de autenticação


            categories.MapEndpoint<CreateCategoryEndpoint>()
                     .MapEndpoint<UpdateCategoryEndpoint>()
                     .MapEndpoint<DeleteCategoryEndpoint>()
                     .MapEndpoint<GetCategoryByIdEndpoint>()
                     .MapEndpoint<GetAllCategoriesEndpoint>();



            var transactions = app.MapGroup("v1/transactions")
                     .WithTags("Transactions")
                     .RequireAuthorization();

            transactions.MapEndpoint<CreateTransactionEndpoint>()
                     .MapEndpoint<UpdateTransactionEndpoint>()
                     .MapEndpoint<DeleteTransactionEndpoint>()
                     .MapEndpoint<GetTransactionByIdEndpoint>()
                     .MapEndpoint<GetTransactionsByPeriodEndpoint>();


        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}

//Comentários para meu controle pessoal
//Classe de extensão para WebApplication, permite que o método MapEndpoints() chame TEndpoint.Map(app) de forma genérica, sem precisar saber os detalhes de cada endpoint.


