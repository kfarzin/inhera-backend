using Inhera.Authentication;
using Inhera.CoreAPI.Config;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Text.RegularExpressions;

var DefaultCorsPolicy = "_DefaultCorsPolicy";
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();



JWTAuthenticationConfig.Init(builder);

AppConfig.Init(builder);
AppConfig.AddCorsSupport(builder);
AppConfig.AddCAPSupport(builder);
AppConfig.AddSwaggerSupport(builder);

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
    options.ModelBinderProviders.Insert(0, new AuthenticatedLabCenterModelBinderProvider());
});

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(DefaultCorsPolicy);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//seed
AppSeeder.Seed(app);

app.Run();

public sealed class SlugifyParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if (value == null) { return null; }
        string? str = value.ToString();
        if (string.IsNullOrEmpty(str)) { return null; }

        return Regex.Replace(str, "([a-z])([A-Z])", "$1-$2").ToLower();
    }
}