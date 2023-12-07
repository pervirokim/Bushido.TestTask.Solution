using Bushido.TestTask.Cloud.CryptoTrading.Interfaces;
using Bushido.TestTask.Cloud.CryptoTrading.Services;
using Bushido.TestTask.Library.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

Console.ForegroundColor = ConsoleColor.Green;
DateTime startedTime = DateTime.Now;
Console.WriteLine($"{startedTime} : App execute started\n\n");
Console.ResetColor();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


OIDCSettings.SetOIDC(builder.Configuration.GetValue<string>("BasicClientId"), builder.Configuration.GetValue<string>("BasicClientSecret"));

builder.Services.AddScoped<IOrderManagerService, OrderManagerService>();
builder.Services.AddScoped<IOrderRepositoryService, OrderRepositoryService>();
builder.Services.AddScoped<IUserBalanceService, UserBalanceService>();
builder.Services.AddScoped<IExchangeService, ExchangeService>();

string dbContextConenctionStringKey = builder.Environment.IsDevelopment() ? "DataBaseLocalConnection" : "DataBaseDevConnection";

builder.Services.AddDbContext<CryptoTradingDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString(dbContextConenctionStringKey)));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();


Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"\n{DateTime.Now} : App execution has been finished");
Console.WriteLine($"{(DateTime.Now - startedTime).TotalSeconds}:seconds\n\n");
Console.ResetColor();

app.Run();
