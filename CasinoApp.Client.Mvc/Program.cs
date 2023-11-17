using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CasinoApp.Client.Mvc.Data;
using Microsoft.Extensions.Configuration;
using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CasinoAppClientMvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CasinoAppClientMvcContext") ?? throw new InvalidOperationException("Connection string 'CasinoAppClientMvcContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
