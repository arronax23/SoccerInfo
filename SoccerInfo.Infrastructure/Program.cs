using Microsoft.EntityFrameworkCore;
using SoccerInfo.FrontendScraper.Utilities;
using SoccerInfo.FrontendScraper;
using SoccerInfo.Infrastructure;
using SoccerInfo.Persistence.Sql;
using SoccerInfo.Shared.CQRS;
using SoccerInfo.Infrastructure.CQRS;
using SoccerInfo.Persistence.Data;
using SoccerInfo.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ImageFetcher>();
builder.Services.AddSingleton<PuppeteerManager>();
builder.Services.AddTransient<ISqlExecutor, SqlExecutor>();
builder.Services.AddScoped<IQueryDispatcher, QueryDispatcher>();
builder.Services.AddScoped<ICommandDispatcher, CommandDispatcher>();


builder.Services.AddApplicationServices();
builder.Services.AddFrontendScraperServices();

builder.Services.AddHttpClient();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterMediatR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDbContext<ApplicationDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
