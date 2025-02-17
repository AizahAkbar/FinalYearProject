using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore;
using FinalYearProject.Models;
using Microsoft.Extensions.DependencyInjection;
using FinalYearProject.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ReviewsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReviewsContext") ?? throw new InvalidOperationException("Connection string 'ReviewsContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BakesContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("BakesContext") ?? throw new InvalidOperationException("Connection string 'BakesContext' not found.")));

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "basket",
    pattern: "basket",
    defaults: new { controller = "Basket", action = "Index" }
);

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

app.MapControllerRoute(
    name: "searchPage",
    pattern: "search",
    defaults: new { controller = "Search", action = "Index" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Bakes}/{action=Index}/{id?}");

app.Run();
