using MVCPro.DAL.Data;
using MVCPro.DAL.Interfaces;
using MVCPro.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using MVCPro.UI.Configs;
using MVCPro.BLL.Services;
using MVCPro.BLL.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICourseRepo, CourseRepo>();
builder.Services.AddScoped<ISessionRepo, SessionRepo>();      
builder.Services.AddScoped<IGradeRepo, GradeRepo>();         
builder.Services.AddScoped<IUserRepo, UserRepo>();           
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ISessionService, SessionService>();
builder.Services.AddScoped<IGradeService, GradeService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.Configure<PaginationSettings>(
    builder.Configuration.GetSection("Pagination"));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
