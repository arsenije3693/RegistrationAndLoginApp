using RegistrationAndLoginApp.Services.BussinesLogicLayer;
using RegistrationAndLoginApp.Services.DataAccessLayer;
using RegistrationAndLoginApp.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register DAO
builder.Services.AddSingleton<IUserDAO, LocalUserDAO>();

// Register UserLogic and let DI inject IUserDAO automatically
builder.Services.AddScoped<UserLogic>();

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
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
