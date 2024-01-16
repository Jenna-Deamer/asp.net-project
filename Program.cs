using Microsoft.EntityFrameworkCore;
using WorldDominion.Models;

var builder = WebApplication.CreateBuilder(args);

//Add sessions 
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); 
    //session will only last for 30 mins. Shopping cart will reset after 30 mins.
    //If they close the browser at any time it resets stored data
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true; //a bit of security. 
    //If their session id and cookie don't match it will reset the session.
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add NySQL
var connectionString = builder.Configuration.GetConnectionString("Default")
?? throw new InvalidOperationException("Connection String not found"); //?? means otherwise. If its not found throw an error

builder.Services.AddDbContext<ApplicationDbContext>
(options => options.UseMySQL(connectionString)); //lambda a function as an argument

var app = builder.Build();

//enable sessions on our requests
app.UseSession();
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
