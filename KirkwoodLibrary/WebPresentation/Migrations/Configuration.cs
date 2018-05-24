namespace WebPresentation.Migrations
{

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Security.Claims;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebPresentation.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebPresentation.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WebPresentation.Models.ApplicationDbContext";
        }

        protected override void Seed(WebPresentation.Models.ApplicationDbContext context)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            // add any existing roles
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Administrator" });
            context.Roles.AddOrUpdate(r => r.Name, new IdentityRole() { Name = "Student" });

            // create the admin user
            string admin = "admin@kirkwood.edu";
            string defaultPassword = "P@ssw0rd";

            if (!context.Users.Any(u => u.UserName.Equals(admin)))
            {
                var user = new ApplicationUser()
                {
                    UserName = admin,
                    Email = admin,
                    FirstName = "Badis",
                    LastName = "Saidani"
                };

                IdentityResult result = userManager.Create(user, defaultPassword);
                context.SaveChanges();
                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Administrator");
                    context.SaveChanges();
                }
            }

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
