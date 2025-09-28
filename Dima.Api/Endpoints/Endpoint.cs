﻿using Microsoft.AspNetCore.Builder;
using Dima.Api.Endpoints.Categories;
using Dima.Api.Common.Api;

namespace Dima.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("/v1/categories")
                               .WithTags("Categories");
                               

            endpoints.MapEndpoint<CreateCategoryEndpoint>()
                     .MapEndpoint<UpdateCategoryEndpoint>()
                     .MapEndpoint<DeleteCategoryEndpoint>()
                     .MapEndpoint<GetCategoryByIdEndpoint>()
                     .MapEndpoint<GetAllCategoriesEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);
            return app;
        }
    }
}
