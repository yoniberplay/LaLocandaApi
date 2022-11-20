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
    public class IDTTContext: IdentityDbContext<AppUser>
    {
        public IDTTContext(DbContextOptions<IDTTContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder m)
        {
            base.OnModelCreating(m);

            m.HasDefaultSchema("IDTT");

            m.Entity<AppUser>(t =>
            {
                t.ToTable("Users");
            });

            m.Entity<IdentityRole>(t =>
            {
                t.ToTable("Roles");
            });

            m.Entity<IdentityUserRole<string>>(t =>
            {
                t.ToTable("UserRoles");
            });

            m.Entity<IdentityUserLogin<string>>(t =>
            {
                t.ToTable("UserLogins");
            });


        }
    }
}
