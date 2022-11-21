using LaLocanda.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaLocanda.Infrastructure.Identity.Contexts
{
    public class IdentityContext: IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("Identity");

            builder.Entity<ApplicationUser>(t =>
            {
                t.ToTable("Users");
            });

            builder.Entity<IdentityRole>(t =>
            {
                t.ToTable("Roles");
            });

            builder.Entity<IdentityUserRole<string>>(t =>
            {
                t.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserLogin<string>>(t =>
            {
                t.ToTable("UserLogins");
            });


        }
    }
}
