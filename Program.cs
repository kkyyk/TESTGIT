using Microsoft.EntityFrameworkCore;
using TESTMVCCORE.Models.DB;
using TESTMVCCORE.Service;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 透過 builder.Services 將服務加入 DI 容器
// DB
builder.Services.AddDbContext<DBContext>(options =>
	options.UseSqlServer(connectionString));

// Add services to the container.  
//要即時修改cshtml要使用AddRazorRuntimeCompilation
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddScoped<PersonaService>();


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
	pattern: "{controller=Home}/{action=List}/{id?}");

app.Run();
