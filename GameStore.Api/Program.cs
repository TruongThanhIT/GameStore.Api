using FluentValidation;
using FluentValidation.AspNetCore;
using GameStore.Api.Application.Services.Games;
using GameStore.Api.Application.Services.Genres;
using GameStore.Api.Application.UseCases.Games;
using GameStore.Api.Application.UseCases.Genres;
using GameStore.Api.Application.Validators;
using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Presentation.Middleware;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
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
        else if(builder.Environment.IsDevelopment())
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        }
    });
});
builder.Services.AddValidation();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateGameValidator>();

// Register Use Cases
builder.Services.AddScoped<CreateGameUseCase>();
builder.Services.AddScoped<UpdateGameUseCase>();
builder.Services.AddScoped<GetGameByIdUseCase>();
builder.Services.AddScoped<ListGamesUseCase>();
builder.Services.AddScoped<DeleteGameUseCase>();
builder.Services.AddScoped<GetGenresUseCase>();

builder.Services.AddScoped<IGameApplicationService, GameApplicationService>();
builder.Services.AddScoped<IGenreApplicationService, GenreApplicationService>();

builder.Services.AddGameStoreDb(builder.Configuration);
builder.Services.AddOpenApi();
var app = builder.Build();

if(app.Environment.IsDevelopment())
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
