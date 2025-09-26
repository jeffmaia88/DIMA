using Dima.Api.Data;
using Dima.Core.Entities;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Handlers;
using Dima.Api.Handlers;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}


var connection = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(connection);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});


builder.Services.AddTransient<ICategoryHandler,CategoryHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.MapGet("/", () => "Hello World!");
app.MapPost("/v1/categories", async (CreateCategoryRequest request, ICategoryHandler handler) => await handler.CreateAsync(request))
                                 .WithName("Categories: Create")
                                 .WithSummary("Cria uma nova categoria")
                                 .Produces<Response<Category>>();



app.MapDelete("v1/categories/{id}", async (long id, ICategoryHandler handler) =>
                                           {
                                               var request = new DeleteCategoryRequest
                                               {
                                                   Id = id
                                               };                                               
                                                return await handler.DeleteAsync(request);
                                            })
                                             .WithName("Categories: Delete")
                                             .WithSummary("Deleta uma categoria existente")
                                             .Produces<Response<Category>>();

app.MapGet("v1/categories/{id}", async (long id, ICategoryHandler handler) =>
                                        {
                                            var request = new GetCategoryByIdRequest
                                            {
                                                Id = id
                                            };
                                            
                                            return await handler.GetByIdAsync(request);
                                        })
                                           .WithName("Categories: Get")
                                           .WithSummary("Busca uma Categoria")
                                           .Produces<Response<Category>>();

app.MapGet("v1/categories/", async (ICategoryHandler handler) =>
{
    var request = new GetAllCategoriesRequest
    {
        UserId = "teste@jeff.com"
    };

    return await handler.GetAllAsync(request);
})
                                           .WithName("Categories: Get ALL")
                                           .WithSummary("Retrona todas as categorias")
                                           .Produces<PagedResponse<List<Category>?>>();


app.MapPut("/v1/categories/{id}", async (long id, UpdateCategoryRequest request, ICategoryHandler handler) =>
                                        {
                                            request.Id = id; 
                                            return await handler.UpdateAsync(request);
                                        }).WithName("Categories: Update")
                                          .WithSummary("Atualiza uma categoria existente")
                                          .Produces<Response<Category>>();





app.Run();






