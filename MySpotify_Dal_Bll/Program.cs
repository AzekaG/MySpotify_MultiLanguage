using Microsoft.EntityFrameworkCore;
using MySpotify.BLL.Interfaces;
using MySpotify.BLL.Services;
using MySpotify.BLL.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

//регистрируем подключение к бд
builder.Services.AddMediaUserContext(connection);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.Name = "Session";

});
//регитсрируем <IUnitOfWork, EFUnitOfWork> через слой BLL
builder.Services.AddUnitOfWorkService();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IGenreService, GenreService>();


// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Media}/{action=Index}/{id?}");

app.Run();

