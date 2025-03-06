using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PersonalFinanceTracker.Models;
using PersonalFinanceTracker.data;
using System;
using System.Threading.Tasks;


static async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roleNames = { "Admin", "User" };
    
    foreach (var roleName in roleNames)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }
}


// Sets up the application with default configurations.
var builder = WebApplication.CreateBuilder(args);

// enable console logging
builder.Logging.AddConsole(); 

// Configure the Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information);
});

// Configure Identity for Authentication
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Configure Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";  // Redirect to login if unauthorized
    options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect for denied access
});


// Add MVC Controllers and Views
builder.Services.AddControllersWithViews();

// Adds support for Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Call Create Roles within a scoped context
// Role manager is only available after app.Build()
// Ensures the roles exist and if they dont then it creates them
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await CreateRoles(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{ 
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Forces all requests to use HTTPS
app.UseHttpsRedirection();
// Serves CSS, JS, and images from wwwroot/
app.UseStaticFiles();

// Enables routing (so the app knows how to handle URLs)
app.UseRouting();
// Enables authentication and authorization (we will configure this later)
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();  // For API controllers
app.MapRazorPages();   // If using Razor Pages
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Default MVC routing

// Maps Razor Pages (so the app can serve UI pages)
app.MapRazorPages();

// Starts the application
app.Run();
