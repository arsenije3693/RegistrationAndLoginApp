using RegistrationAndLoginApp.Services.BussinesLogicLayer;
using RegistrationAndLoginApp.Services.DataAccessLayer;
using RegistrationAndLoginApp.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DAO
builder.Services.AddSingleton<IUserDAO, LocalUserDAO>();

// Register UserLogic and let DI inject IUserDAO automatically
builder.Services.AddScoped<UserLogic>();

// Setup authentication and authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Path to redirect when user is not authenticated
        options.LoginPath = "/Login/Login";

        // Path to redirect when user is forbidden (access denied)
        options.AccessDeniedPath = "/Login/Login";
    });

// Enable authorization system
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Make sure authentication runs before authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
