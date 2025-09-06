using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;
using SoccerInfo.API;
using SoccerInfo.Application;
using SoccerInfo.BackendScraper;
using SoccerInfo.Domain;
using SoccerInfo.FrontendScraper;
using SoccerInfo.Infrastructure;
using SoccerInfo.Infrastructure.CQRS;
using SoccerInfo.Infrastructure.Language;
using SoccerInfo.Infrastructure.Swagger;
using SoccerInfo.Persistence;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Rapid.Application;
using SoccerInfo.Shared.CQRS;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(cfg => cfg.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();


builder.Services.AddRapidApiServices();

builder.Services.AddApiServices();
builder.Services.AddDomainServices();
builder.Services.AddApplicationServices();
builder.Services.AddFrontendScraperServices();
builder.Services.AddBackendScraperServices();
builder.Services.AddPersistenceServices();

builder.Services.AddHttpClient();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterMediatR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(SwaggerHelper.Configure);

builder.Services.AddJobs();

builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
        options
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseLazyLoadingProxies()
            .UseSqlServer(builder.Configuration.GetConnectionString("Default")));

LanguageHelper.SetCultureToPL();

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
    app.UseSwaggerUI(SwaggerHelper.ConfigureUI);
}

//app.UseHttpsRedirection()

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();
