using FluentValidation;
using FluentValidation.AspNetCore;
using GameStore.Api.Application.Services.Games;
using GameStore.Api.Application.Services.Genres;
using GameStore.Api.Application.UseCases.Games;
using GameStore.Api.Application.UseCases.Genres;
using GameStore.Api.Application.Validators;
using GameStore.Api.Configuration;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Presentation.Middleware;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var appConfig = builder.Configuration.Get<ApplicationConfiguration>() ?? new ApplicationConfiguration();
var allowedOrigins = appConfig.AllowedOrigins;

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        if (allowedOrigins.Length > 0)
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
        else
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    });
});

builder.Services.AddApplicationConfiguration(builder.Configuration);

builder.Services.AddValidation();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateGameValidator>();

builder.Services.AddApplicationDependencies();

builder.Services.AddGameStoreDb(builder.Configuration);
builder.Services.AddOpenApi();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

await app.MigrateDbAsync();

app.Run();
