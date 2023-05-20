using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using appfunko.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using StackExchange.Redis;
using appfunko.Service;
using appfunko.Integration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

//Registro mi logica customizada y reuzable
builder.Services.AddScoped<ProductoService, ProductoService>();
builder.Services.AddScoped<CurrencyExchangeApiIntegration, CurrencyExchangeApiIntegration>();


builder.Services.AddStackExchangeRedisCache(options =>
    {
        var configuration = builder.Configuration.GetSection("Caching:RedisCache");
        options.Configuration = configuration["ConnectionString"];
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
