using GraphQLTMS.Core.Authorization;
using GraphQLTMS.Core.Domain;
using GraphQLTMS.Shared.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLTMS.Core.Data
{
    public static class DbInitializer
    {
        public static async Task Populate(IApplicationBuilder app, IConfiguration config, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                await SeedData(
                    serviceScope.ServiceProvider.GetRequiredService<TMSDbContext>(),
                    //serviceScope.ServiceProvider.GetService<ApplicationUserDbContext>(),
                    //serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>(),
                    //serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>(),
                    config, 
                    env
                );
            }
        }

        private static async Task SeedData(TMSDbContext context, IConfiguration config, IWebHostEnvironment env)
        {
            try
            {
                context.Database.Migrate();
                //userContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("--> Could not run migrations: " + ex.Message);
            }

            PopulateTMSDb(context);
            //PopulateUserDb(userContext, userManager, roleManager);
        }

        private static void PopulateUserDb(ApplicationUserDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (context.Users.Any())
            {
                return;
            }

            if (!context.Roles.Any())
            {
                roleManager.CreateAsync(new IdentityRole { Name = TMSRoles.Admin }).Wait();
                roleManager.CreateAsync(new IdentityRole { Name = TMSRoles.Customer }).Wait();
            }

            if (!context.Users.Any())
            {
                // seed users
                var kyle = new ApplicationUser
                {
                    UserName = "mkmorton91@gmail.com",
                    Email = "mkmorton91@gmail.com",
                    EmailConfirmed = true
                };
                userManager.CreateAsync(kyle, "Test123!").Wait();

                // seed users
                userManager.AddToRoleAsync(kyle, TMSRoles.Admin).Wait();
            }
        }

        private static void PopulateTMSDb(TMSDbContext context)
        {
            if (context.Shipments.Any())
            {
                return;
            }

            context.Shipments.Add(new Shipment
            {
                Origin = "Little Rock, AR 72211",
                Destination = "Chicago, IL 60606",
                Revenue = 5000,
                Cost = 4000,
                WeightInPounds = 1500,
                PickupDate = DateTime.Now.AddDays(1),
                DeliveryDate = DateTime.Now.AddDays(5),
                Customer = new Customer
                {
                    Name = "ABC Windows",
                    CustomerNumber = "ABC123"
                },
                Carrier = new Carrier
                {
                    Name = "T Force",
                    Scac = "UPFG",
                    SafetyRating = "Safe",
                    IsApproved = true,
                }
            });

            context.SaveChanges();
        }
    }
}
