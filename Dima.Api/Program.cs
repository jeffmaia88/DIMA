using Dima.Api.Data;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Handlers;
using Dima.Api.Handlers;
using Dima.Api.Endpoints;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}


var connection = builder.Configuration.GetConnectionString("DefaultConnection");




builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.CustomSchemaIds(n => n.FullName);
});

builder.Services.AddAuthentication(IdentityConstants.ApplicationScheme).AddIdentityCookies(); // utiliza Cookies para autenticação
builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(connection);
});


builder.Services.AddTransient<ICategoryHandler,CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", () => new { message = "OK" });
    app.MapEndpoints();
}
    





app.Run();






