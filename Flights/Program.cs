using Flights.Details;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Flights.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
/*builder.Services.AddDbContext<Flightdetailsdbcontext>(options=> 
           options.UseSqlServer(builder.Configuration
           .GetConnectionString("FlightDataConnectionString")));*/
builder.Services.AddDbContext<Flightdetailsdbcontext>(options =>
           options.UseInMemoryDatabase("Flights"));

builder.Services.AddTransient<IMethods,crudmethods>();
builder.Services.AddTransient<ICsvMethods, Csvmethods>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
