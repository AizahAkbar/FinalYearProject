using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using FinalYearProject.Models;
using Microsoft.Extensions.DependencyInjection;
using FinalYearProject.Data;
using FinalYearProject.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FypContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FypContext") ?? throw new InvalidOperationException("Connection string 'FypContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
// builder.Services.AddSession(options =>
// {
//     options.IdleTimeout = TimeSpan.FromMinutes(30);
//     options.Cookie.HttpOnly = true;
//     options.Cookie.IsEssential = true;
// });

// Dependency injecting interface and class
// Singleton - one object to inject 
// Transient - every call will be a new object (creates service class every time it needs it)
builder.Services.AddTransient<IBakeRepository, BakeRepository>();
builder.Services.AddTransient<IBakeService, BakeService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

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

app.UseSession();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "basket",
//    pattern: "basket",
//    defaults: new { controller = "Basket", action = "Index" }
//);

app.MapControllerRoute(
    name: "orderForm",
    pattern: "order/form",
    defaults: new { controller = "Order", action = "Form" }
);

app.MapControllerRoute(
    name: "orderSummary",
    pattern: "order",
    defaults: new { controller = "Order", action = "Index" }
);

app.MapControllerRoute(
    name: "payment",
    pattern: "payment",
    defaults: new { controller = "Order", action = "Payment" }
);

//app.MapControllerRoute(
//    name: "searchPage",
//    pattern: "search",
//    defaults: new { controller = "Search", action = "Index" }
//);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Bakes}/{action=Index}/{id?}");

app.Run();
