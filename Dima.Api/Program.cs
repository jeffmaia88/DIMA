using Dima.Api.Data;
using Dima.Core.Entities;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddTransient<Handler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/", () => "Hello World!");
app.MapPost("/v1/categories", (Request request, Handler handler) => handler.Handle(request))
                                 .WithName("Categories: Create")
                                 .WithSummary("Cria uma nova categoria")
                                 .Produces<Response>();


app.Run();


public class Request
{
    public  string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}


public class Response
{
    public long Id { get; set; }
    public string Title{ get; set; } = string.Empty;

}

public class Handler(AppDbContext context)
{
    public Response Handle(Request request)
    {
        var category = new Category
        {
            Title = request.Title,
            Description = request.Description,
        };

        context.Categories.Add(category);
        context.SaveChanges();

        return new Response
        {
            Id = 1,
            Title = request.Title
        };
    }
}