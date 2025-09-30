using Dima.Api.Endpoints.Categories;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints.Transactions;

namespace Dima.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var categories= app.MapGroup("/v1/categories")
                               .WithTags("Categories");
                               

            categories.MapEndpoint<CreateCategoryEndpoint>()
                     .MapEndpoint<UpdateCategoryEndpoint>()
                     .MapEndpoint<DeleteCategoryEndpoint>()
                     .MapEndpoint<GetCategoryByIdEndpoint>()
                     .MapEndpoint<GetAllCategoriesEndpoint>();



            var transactions = app.MapGroup("v1/transactions")
                     .WithTags("Transactions");

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


