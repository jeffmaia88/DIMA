using Dima.Api.Data;
using Microsoft.EntityFrameworkCore;
using Dima.Core.Handlers;
using Dima.Api.Handlers;
using Dima.Api.Endpoints;
using Microsoft.AspNetCore.Identity;
using Dima.Api.Models;
using System.Security.Claims;

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

builder.Services.AddIdentityCore<User>().AddRoles<IdentityRole<long>>().AddEntityFrameworkStores<AppDbContext>().AddApiEndpoints();

builder.Services.AddTransient<ICategoryHandler,CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapGet("/", () => new { message = "OK" });
    app.MapEndpoints();
    app.MapGroup("v1/identity").MapIdentityApi<User>().WithOpenApi();

    app.MapGroup("v1/identity")
       .WithTags("Identity")
       .MapPost("/logout", async (SignInManager<User> signInManager, UserManager<User> userManager,RoleManager<IdentityRole<long>> roleManager) =>
    {
        await signInManager.SignOutAsync();
        return Results.Ok();

    }).RequireAuthorization();

    app.MapGroup("v1/identity")
       .WithTags("Identity")
       .MapGet("/roles", (ClaimsPrincipal user) =>
       {
           if (user.Identity is null || !user.Identity.IsAuthenticated)
               return Results.Unauthorized();

           var roles = user.FindAll(ClaimTypes.Role)
                               .Select(c => new
           {
               c.Issuer,
               c.OriginalIssuer,
               c.Type,
               c.Value,
               c.ValueType
           });

           return TypedResults.Json(roles);

       }).RequireAuthorization();


}
    





app.Run();






