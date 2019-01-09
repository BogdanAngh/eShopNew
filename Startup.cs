using e_Shop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(e_Shop.Startup))]
namespace e_Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // Se apeleaza o metoda in care se adauga contul de administrator
            //si rolurile aplicatiei
            createAdminUserAndApplicationRoles();
        }

        private void createAdminUserAndApplicationRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new
                RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new
                UserStore<ApplicationUser>(context));
            // Se adauga rolurile aplicatiei
            if (!roleManager.RoleExists("Administrator"))
            {
                // Se adauga rolul de administrator
                var role = new IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);
                // se adauga utilizatorul administrator
                var user = new ApplicationUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                var adminCreated = UserManager.Create(user, "Administrator!");
                if (adminCreated.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Administrator");
                }
            }
            if (!roleManager.RoleExists("Collab"))
            {
                // Se adauga rolul de colaborator
                var role = new IdentityRole();
                role.Name = "Collab";
                roleManager.Create(role);
                // se adauga utilizatorul colaborator
                var user = new ApplicationUser();
                user.UserName = "collab@collab.com";
                user.Email = "collab@collab.com";
                var collabCreated = UserManager.Create(user, "Collab!");
                if (collabCreated.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Collab");
                }
            }

            if (!roleManager.RoleExists("User"))
            {
                var role = new IdentityRole();
                role.Name = "User";
                roleManager.Create(role);
            }
        }
    }
    }
