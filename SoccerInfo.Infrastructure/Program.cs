using Serilog;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpOverrides;
using SoccerInfo.API;
using SoccerInfo.FrontendScraper;
using SoccerInfo.Infrastructure;
using SoccerInfo.Persistence.Sql;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Infrastructure.CQRS;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Application;
using SoccerInfo.Infrastructure.Swagger;

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
builder.Services.AddSwaggerGen(SwaggerHelper.Setup);

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
