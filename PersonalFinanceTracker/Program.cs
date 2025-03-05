using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using PersonalFinanceTracker.Models;


// Sets up the application with default configurations.
var builder = WebApplication.CreateBuilder(args);

// Configure the Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => 
{
    options.SignIn.RequireConfirmedAccount = false;

}).AddEntityFrameworkStores<ApplicationDbContext>();

// Add MVC Controllers and Views
builder.Services.AddControllersWithViews();

// Adds support for Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
app.UseAuthorization();

app.MapControllers();  // For API controllers
app.MapRazorPages();   // If using Razor Pages
app.MapDefaultControllerRoute(); // Default MVC routing

// Maps Razor Pages (so the app can serve UI pages)
app.MapRazorPages();

// Starts the application
app.Run();
