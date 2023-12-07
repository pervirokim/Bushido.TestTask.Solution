using Bushido.TestTask.Cloud.ClientApp.Interfaces;
using Bushido.TestTask.Cloud.ClientApp.Services;
using Bushido.TestTask.Cloud.CryptoTrading.Interfaces;
using Bushido.TestTask.Cloud.CryptoTrading.Services;
using Bushido.TestTask.Library.Core.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<IOrderService, OrderService>(); //singlton it is just for testing, in normal project we must contain sessions
builder.Services.AddSingleton<IAuthService, AuthService>(); //singlton it is just for testing, in normal project we must contain sessions
builder.Services.AddSingleton<IBalanceService, BalanceService>(); //singlton it is just for testing, in normal project we must contain sessions

OIDCSettings.SetOIDC(builder.Configuration.GetValue<string>("BasicClientId"), builder.Configuration.GetValue<string>("BasicClientSecret"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
