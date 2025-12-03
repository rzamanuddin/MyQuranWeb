using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MyQuranWeb.Blazor.Data;
using MyQuranWebRepository.Interfaces;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Library.Options;
using MyQuranWebRepository;
using Blazored.LocalStorage;
using System.Text.Json;
using MyQuranWeb.Blazor.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Blazored.Toast;
using MyQuranWeb.Blazor.Pages.Qurans;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<CopyService>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddBlazoredLocalStorage(); // local storage
builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true);
builder.Services.Configure<AppSettingOption>(builder.Configuration.GetSection(AppSettingOption.APP_SETTING));
builder.Services.AddRepository(builder.Configuration);

builder.Services.AddScoped<ClipboardService>();
builder.Services.AddSweetAlert2(
    options =>
    {
        options.Theme = SweetAlertTheme.Default;
    });
builder.Services.AddBlazoredToast();
builder.Services.AddBlazorBootstrap();

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
