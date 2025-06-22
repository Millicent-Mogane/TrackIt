using Microsoft.EntityFrameworkCore;
using TrackItApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Register services into the container

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation(); // Allows automatic .cshtml page refresh on changes during development

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // Registers EF Core with SQL Server using a connection string from appsettings.json

builder.Services.AddSession(); // Enables session management (for storing user data between requests)

var app = builder.Build();

// Configure the HTTP request pipeline

app.UseStaticFiles(); // Serves static files like images, CSS, JS from wwwroot
app.UseRouting();     // Enables routing middleware to direct requests to controllers/actions
app.UseSession();     // Enables reading and writing session data

// Defines the default route pattern
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run(); // Starts the application
