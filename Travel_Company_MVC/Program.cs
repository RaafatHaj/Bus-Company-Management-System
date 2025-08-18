using Microsoft.AspNetCore.Identity;
using TravelCompany.Infrastructure.Persistence.Entities;
using TravelCompany.Infrastructure.Persistence;
using TravelCompany.Infrastructure;
using TravelCompany.Application;
using UoN.ExpressiveAnnotations.NetCore.DependencyInjection;
using Travel_Company_MVC.Mappping;
using Travel_Company_MVC;
using Travel_Company_MVC.Seeds;

var builder = WebApplication.CreateBuilder(args);


//var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));


//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ApplicationDbContext>();

//// Add services to the container.
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));
//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
//builder.Services.AddControllersWithViews();


builder.Services.RegisterInfrastructureServices(builder.Configuration);
builder.Services.RegisterApplicationServices();
builder.Services.RegisterWebServices(builder);
builder.Services.AddAutoMapper(typeof(MappingProfile));


/*builder.Services.AddControllersWithViews();
*/
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

app.UseAuthorization();

//JobScheduler.SyncScheduledJobs();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using( var scope = scopeFactory.CreateScope())
{

    var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManger = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();


   await DefaultRoles.SeedAsync(roleManger);
   await DefaultUsers.SeedAdminAsync(userManger);
}



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
    //pattern: "{controller=Trips}/{action=ScheduledTrips}/{id?}");
app.MapRazorPages();

app.Run();
