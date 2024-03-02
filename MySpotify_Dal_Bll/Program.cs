using Microsoft.EntityFrameworkCore;
using MySpotify.BLL.Interfaces;
using MySpotify.BLL.Services;
using MySpotify.BLL.Infrastructure;
using MySpotify;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

//регистрируем подключение к бд
builder.Services.AddMediaUserContext(connection);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
//регитсрируем <IUnitOfWork, EFUnitOfWork> через слой BLL
builder.Services.AddUnitOfWorkService();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMediaService, MediaService>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddSignalR();


// Добавляем сервисы MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseSession();
app.UseHttpsRedirection();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Media}/{action=Index}/{id?}");
app.UseStaticFiles();
app.MapHub<NotificationHub>("/notification");

app.Run();

