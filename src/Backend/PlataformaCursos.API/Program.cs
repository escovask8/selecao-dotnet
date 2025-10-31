using System.Reflection;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using PlataformaCursos.Application.Commands.Estudantes;
using PlataformaCursos.Application.Common.Mappings;
using PlataformaCursos.Application.Handlers;
using PlataformaCursos.Domain.Common;
using PlataformaCursos.Infrastructure;
using Serilog;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger Configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Plataforma de Cursos API",
        Version = "v1",
        Description = "API para gerenciamento de cursos online com Clean Architecture",
        Contact = new OpenApiContact
        {
            Name = "Plataforma Cursos",
            Email = "contato@plataformacursos.com"
        }
    });

    // Incluir comentários XML se disponível
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Application Layer
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
    cfg.RegisterServicesFromAssembly(typeof(CadastrarEstudanteCommandHandler).Assembly);
});

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddValidatorsFromAssembly(typeof(CadastrarEstudanteCommandValidator).Assembly);

// Infrastructure Layer
builder.Services.AddInfrastructure();

// Memory Cache
builder.Services.AddMemoryCache();

// Health Checks
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Plataforma de Cursos API v1");
        c.RoutePrefix = string.Empty; // Swagger na raiz
    });
}

app.UseCors("AllowAngularApp");

app.UseAuthorization();

// Global Exception Handler
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        var response = new
        {
            error = "Ocorreu um erro interno no servidor",
            message = exception?.Message,
            stackTrace = app.Environment.IsDevelopment() ? exception?.StackTrace : null
        };

        Log.Error(exception, "Erro não tratado: {Message}", exception?.Message);

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    });
});

app.MapControllers();

app.MapHealthChecks("/health");

try
{
    Log.Information("Iniciando aplicação");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Aplicação terminada inesperadamente");
}
finally
{
    Log.CloseAndFlush();
}

// Expor Program para testes de integração
public partial class Program { }

