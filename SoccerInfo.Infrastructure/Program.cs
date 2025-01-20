using Microsoft.EntityFrameworkCore;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.FrontendScraper;
using SoccerInfo.Infrastructure;
using SoccerInfo.Persistence.Sql;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Infrastructure.CQRS;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Application;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using SoccerInfo.API;
using Microsoft.OpenApi.Models;
using SoccerInfo.API.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(cfg => cfg.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddTransient<ISqlExecutor, SqlExecutor>();
builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();


builder.Services.AddApiServices();
builder.Services.AddApplicationServices();
builder.Services.AddFrontendScraperServices();

builder.Services.AddHttpClient();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterMediatR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key needed to access the endpoints. Add it to the request header with the name 'X-API-KEY'.",
        Type = SecuritySchemeType.ApiKey,
        Name = "X-Api-Key",
        In = ParameterLocation.Header,
        Scheme = "ApiKey"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                Scheme = "ApiKey",
                Name = "ApiKey",
                In = ParameterLocation.Header
            },
            new List<string>(){}
        }
    });
});

builder.Services
    .AddDbContext<ApplicationDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
