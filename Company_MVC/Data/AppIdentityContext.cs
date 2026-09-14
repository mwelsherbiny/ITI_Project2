using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Company_MVC.Data
{
    public class AppIdentityContext : IdentityDbContext<IdentityUser>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}