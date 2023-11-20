using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CasinoApp.Client.Mvc.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

var builder = WebApplication.CreateBuilder(args);

// Configurar la conexión a la base de datos
builder.Services.AddDbContext<CasinoAppClientMvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CasinoAppClientMvcContext") ?? throw new InvalidOperationException("Connection string 'CasinoAppClientMvcContext' not found.")));

// Configurar la autenticación y autorización
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/Usuario/Login"; 
});

builder.Services.AddAuthorization();

// Añadir servicios MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline de solicitud HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Habilitar la autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
