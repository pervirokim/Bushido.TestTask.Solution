using Bushido.TestTask.Cloud.Authentication.Auth;
using Bushido.TestTask.Cloud.Authentication.Interfaces;
using Bushido.TestTask.Cloud.Authentication.Services;
using Microsoft.AspNetCore.Authentication;
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

builder.Services.AddAuthentication(AuthOptions.AuthenticationScheme)
    .AddScheme<AuthenticationSchemeOptions, AuthenticationHandlerService>(AuthOptions.AuthenticationScheme, (options) =>
    {
    });

builder.Services.AddScoped<IAuthService, AuthService>(); //main servise

string dbContextConenctionStringKey = builder.Environment.IsDevelopment() ? "DataBaseLocalConnection" : "DataBaseDevConnection";

builder.Services.AddDbContext<AuthenticationDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString(dbContextConenctionStringKey)));




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"\n{DateTime.Now} : App execution has been finished");
Console.WriteLine($"{(DateTime.Now - startedTime).TotalSeconds}:seconds\n\n");
Console.ResetColor();
app.Run();
