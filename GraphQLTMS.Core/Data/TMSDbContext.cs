using GraphQLTMS.Shared.Domain;
using Microsoft.EntityFrameworkCore;

namespace GraphQLTMS.Core.Data
{
    public class TMSDbContext : DbContext
    {
        public TMSDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Carrier> Carriers { get; set; }
        public DbSet<Shipment> Shipments { get; set; }

    }
}
