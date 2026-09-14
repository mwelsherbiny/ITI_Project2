using Company_MVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace Company_MVC
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<CompanyContext>(options => options.UseSqlServer(connectionString));

            //register Idenetity context
            builder.Services.AddDbContext<AppIdentityContext>(options =>  { options.UseSqlServer( builder.Configuration.GetConnectionString("IdentityConnection"));  });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // Password configurations
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;

                // Lockout configurations
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            })
            .AddEntityFrameworkStores<AppIdentityContext>() // Where Identity data is stored
            .AddDefaultTokenProviders(); // Tokens for email confirmation and password reset
            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/Account/AccessDenied";
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
            app.UseRouting();

            app.UseAuthentication();    
            app.UseAuthorization();


            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var companyContext =
                    services.GetRequiredService<CompanyContext>();

                await companyContext.Database.MigrateAsync();

                var identityContext =
                    services.GetRequiredService<AppIdentityContext>();

                await identityContext.Database.MigrateAsync();

                await SeedData.Initialize(services);
            }

            app.Run();
        }
    }
}
