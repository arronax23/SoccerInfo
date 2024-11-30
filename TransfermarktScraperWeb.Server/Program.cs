using Microsoft.EntityFrameworkCore;
using SoccerInfo.Extractor.Utilities;
using SoccerInfo.Extractor;
using SoccerInfo.Infrastructure;
using SoccerInfoWeb.Server.Data;
using SoccerInfo.Infrastructure.Sql;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<TransfermarktExtractor>();
builder.Services.AddScoped<ImageFetcher>();
builder.Services.AddSingleton<PuppeteerManager>();
builder.Services.AddTransient<ISqlExecutor, SqlExecutor>();

builder.Services.AddHttpClient();
builder.Services.RegisterAutoMapper();
builder.Services.RegisterMediatR();

//using (var sp = builder.Services.BuildServiceProvider()){
//    var t = sp.GetRequiredService<TransfermarktExtractor>();
//    await t.Extarct();
//}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDbContext<ApplicationDbContext>(options => 
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

builder.Services
    .AddControllers()
    .AddApplicationPart(Assembly.GetAssembly(typeof(SoccerInfo.API.IAssemblyMarker))!);


app.MapFallbackToFile("/index.html");

app.Run();
