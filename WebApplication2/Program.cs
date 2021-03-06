using WebApplication2;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebApplication2.Models;
using WebApplication2.ModelView;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DBLibraryContext>(option => option.UseSqlServer( // connection for default connection which using builders 
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddDbContext<IdentityContext>(option => option.UseSqlServer( // connection for identity connection which using builders 
    builder.Configuration.GetConnectionString("identityConnection")));
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IExcel, ExcelCollection>();
builder.Services.AddIdentity<Users, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 5;   // ??????????? ?????
    opt.Password.RequireNonAlphanumeric = false;   // ????????? ?? ?? ?????????-???????? ???????
    opt.Password.RequireLowercase = false; // ????????? ?? ??????? ? ?????? ????????
    opt.Password.RequireUppercase = false; // ????????? ?? ??????? ? ??????? ????????
    opt.Password.RequireDigit = false; // ????????? ?? ?????
}).AddEntityFrameworkStores<IdentityContext>();

var app = builder.Build();
using (var scope = app.Services.CreateScope()) {

var services = scope.ServiceProvider;
   
    try
    {
        var userManager = services.GetRequiredService<UserManager<Users>>();
        var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await RoleInitializer.InitializeAsync(userManager, rolesManager);

    }
    catch (Exception ex) {
    var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database" + DateTime.Now.ToString());
    }
}


    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles(); //share static files 
app.UseAuthentication();//login
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // pattern connection and ll pick up controllers 

app.Run();
