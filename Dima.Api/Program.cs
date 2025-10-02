using Dima.Api;
using Dima.Api.Common.Api;
using Dima.Api.Endpoints;


var builder = WebApplication.CreateBuilder(args);

builder.AddConfiguration();
builder.AddSecurity();
builder.AddDataContext();
builder.AddCrossOrigin();
builder.AddDocumentation();
builder.AddServices();

//builder.Configuration.AddUserSecrets<Program>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ConfigureDevEnvironment();
}

app.UseCors(ApiConfiguration.CorsPolicyName);
app.MapEndpoints();
app.UseSecurity();

app.Run();






