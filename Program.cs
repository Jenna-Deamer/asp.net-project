using Microsoft.EntityFrameworkCore;
using WorldDominion.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add NySQL
var connectionString = builder.Configuration.GetConnectionString("Default")
?? throw new InvalidOperationException("Connection String not found"); //?? means otherwise. If its not found throw an error

builder.Services.AddDbContext<ApplicationDbContext>
(options => options.UseMySQL(connectionString)); //lambda a function as an argument

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
