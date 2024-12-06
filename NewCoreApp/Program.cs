using ClassDAL;
using Data;
using InfraStucture.Contract;
using InfraStucture.Repository;
using Microsoft.EntityFrameworkCore;
using NewCoreApp.Configurations;
using Microsoft.AspNetCore.SignalR;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.InstallServices(builder.Configuration, typeof(IServiceInstaller).Assembly);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); // Replace UnitOfWork with your actual implementation
builder.Services.AddScoped<dbRepository, IdbRepository>();
builder.Services.AddDbContext<UserMgMtContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserManagementDb")));
// Configure services for the second DbContext
builder.Services.AddDbContext<SchoolEducationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SecondConnection")));
builder.Services.AddScoped<IdbRepository>();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Keeps property names as they are in models
});
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
