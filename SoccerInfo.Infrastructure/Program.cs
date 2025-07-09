using Serilog;
using Quartz;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using SoccerInfo.API;
using SoccerInfo.FrontendScraper;
using SoccerInfo.Infrastructure;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Infrastructure.CQRS;
using SoccerInfo.Application;
using SoccerInfo.Infrastructure.Swagger;
using SoccerInfo.Domain;
using SoccerInfo.Persistence;
using SoccerInfo.BackendScraper;
using SoccerInfo.Persistence.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog(cfg => cfg.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();

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
builder.Services.AddSwaggerGen(SwaggerHelper.Setup);

builder.Services.AddJobs();

builder.Services
    .AddDbContext<ApplicationDbContext>(options =>
        options
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseLazyLoadingProxies()
            .UseSqlServer(builder.Configuration.GetConnectionString("Default")));

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
