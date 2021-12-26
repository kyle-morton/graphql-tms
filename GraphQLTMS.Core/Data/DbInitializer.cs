using GraphQLTMS.Shared.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
                await SeedData(serviceScope.ServiceProvider.GetService<TMSDbContext>(), config, env);
            }
        }

        private static async Task SeedData(TMSDbContext context, IConfiguration config, IWebHostEnvironment env)
        {
            try
            {
                context.Database.Migrate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("--> Could not run migrations: " + ex.Message);
            }

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
