using BusinessAccessLayer;
using CommonEntities;
using DataAccessLayer;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using EmployeeManagementSystem.Filters;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Custom Exception
builder.Services.AddControllersWithViews(config => config.Filters.Add(typeof(CustomExceptionFilter)));

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("GorestApiSettings"));
builder.Services.AddScoped<IEmployee, Employee>();
builder.Services.AddSingleton<IApiClient, ApiClient>();


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

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Employee}/{action=Index}/{id?}");

app.Run();
