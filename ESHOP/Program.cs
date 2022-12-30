using System.Security.Claims;
using ESHOP.Data;
using ESHOP.Services;
using ESHOP.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<EShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EShopConnection"));
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Account/Login";
        option.LogoutPath = "/Account/Logout";
        option.ExpireTimeSpan = TimeSpan.FromDays(15);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    // Do work that doesn't write to the Response.
    if (context.Request.Path.StartsWithSegments("/Admin"))
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            context.Response.Redirect("/Account/Login");
        }
        else if (!bool.Parse(context.User.FindFirstValue("IsAdmin")))
        {
            context.Response.Redirect("/Account/Login");
        }
    }
    await next.Invoke();
    // Do logging or other work that doesn't write to the Response.
});

app.UseEndpoints(endpoints =>
    {
        app.MapRazorPages();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    }
);


app.Run();