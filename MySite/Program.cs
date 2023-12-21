using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using MySite.Infrastructure.Extensions;
using Serilog;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using MySite.Services.EmailSender;
using MySite.Services.FileUploadServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

builder.Services.AddServises();
builder.Services.AddScoped<IFileUploadService, LocalFileUploadService>();
builder.Services.AddRazorPages();


builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
   
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";

 });
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] {

        new CultureInfo("en-US"),
        new CultureInfo("uk-UA"),
    };
    options.DefaultRequestCulture = new RequestCulture("en-US");

    options.SupportedCultures = supportedCultures;
});

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
     .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

app.UseRequestLocalization();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
