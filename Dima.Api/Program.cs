using Dima.Api.Data;
using Dima.Core.Entities;
using Dima.Core.Requests.Categories;
using Dima.Core.Requests;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Handlers;
using Dima.Api.Handlers;
using Dima.Api.Endpoints;

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

    app.MapGet("/", () => new { message = "OK" });
    app.MapEndpoints();
}
    





app.Run();






